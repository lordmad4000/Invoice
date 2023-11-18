using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Invoices.Exceptions;
using SimplexInvoice.Domain.Entities;
using SimplexInvoice.Domain.Interfaces;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.Invoices.Commands;
public class InvoiceRegisterHandler : IRequestHandler<InvoiceRegisterCommand, InvoiceDto>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IAppConfigurationRepository _configurationRepository;
    private readonly IDocumentService _documentService;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public InvoiceRegisterHandler(IInvoiceRepository invoiceRepository,
                                  IAppConfigurationRepository configurationRepository,
                                  IDocumentService documentService,
                                  IMapper mapper,
                                  ICustomLogger logger)
    {
        _invoiceRepository = invoiceRepository;
        _configurationRepository = configurationRepository;
        _documentService = documentService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<InvoiceDto> Handle(InvoiceRegisterCommand request, CancellationToken cancellationToken)
    {
        ICollection<InvoiceLine> invoiceLines = _mapper.Map<ICollection<InvoiceLine>>(request.InvoiceLines);
        // TODO A�adir servicio para evitar concurrencia del numero de factura
        AppConfiguration configuration = await _configurationRepository.GetAsync();
        if (configuration == null)
            throw new InvoiceRegisteringException($"Error registering the Invoice. Could not read configuration.");

        string invoiceNumber = _documentService.GetNextInvoiceNumber(configuration.LastInvoiceNumber);
        Invoice invoice = Invoice.Create(invoiceNumber,
                                         request.Description,
                                         request.CompanyName,
                                         request.CompanyIdDocumentType,
                                         request.CompanyDocumentNumber,
                                         new Address(request.CompanyStreet,
                                                     request.CompanyCity,
                                                     request.CompanyState,
                                                     request.CompanyCountry,
                                                     request.CompanyPostalCode),
                                         new PhoneNumber(request.CompanyPhone),
                                         new EmailAddress(request.CompanyEmail),
                                         request.CustomerFullName,
                                         request.CustomerIdDocumentType,
                                         request.CustomerDocumentNumber,
                                         new Address(request.CustomerStreet,
                                                     request.CustomerCity,
                                                     request.CustomerState,
                                                     request.CustomerCountry,
                                                     request.CustomerPostalCode),
                                         new PhoneNumber(request.CustomerPhone),
                                         new EmailAddress(request.CustomerEmail),
                                         invoiceLines);

        configuration.LastInvoiceNumber = invoiceNumber;
        InvoiceDto invoiceDto = _mapper.Map<InvoiceDto>(await _invoiceRepository.AddAsync(invoice, cancellationToken));
        _configurationRepository.Update(configuration);
        if (await _invoiceRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new InvoiceRegisteringException($"Error registering the Invoice.");

        _logger.Debug(@$"Invoice Registered successfully with data: {invoice}");

        return invoiceDto;
    }
}