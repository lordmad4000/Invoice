using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Invoices.Exceptions;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Invoices.Commands;
public class InvoiceRegisterHandler : IRequestHandler<InvoiceRegisterCommand, InvoiceDto>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public InvoiceRegisterHandler(IInvoiceRepository invoiceRepository, 
                                 IMapper mapper, 
                                 ICustomLogger logger)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<InvoiceDto> Handle(InvoiceRegisterCommand request, CancellationToken cancellationToken)
    {
        ICollection<InvoiceLine> invoiceLines = _mapper.Map<ICollection<InvoiceLine>>(request.InvoiceLines);
        Invoice invoice = Invoice.Create(request.Number,
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
        
        InvoiceDto invoiceDto = _mapper.Map<InvoiceDto>(await _invoiceRepository.AddAsync(invoice, cancellationToken));
        if (await _invoiceRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new InvoiceRegisteringException($"Error registering the Invoice.");

        _logger.Debug(@$"Invoice Registered successfully with data: {invoice}");

        return invoiceDto;
    }
}