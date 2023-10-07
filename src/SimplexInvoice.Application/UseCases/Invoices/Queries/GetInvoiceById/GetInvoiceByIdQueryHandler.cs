using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Exceptions;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SimplexInvoice.Application.Invoices.Queries;
public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetInvoiceByIdQueryHandler(IInvoiceRepository invoiceRepository,
                                     IMapper mapper,
                                     ICustomLogger logger)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetAsync(c => c.Id == request.Id, cancellationToken, true, $"Id=={request.Id}", new string[] { "InvoiceLines" });
        if (invoice is null)
            throw new NotFoundException($"Invoice with id {request.Id} was not found");

        invoice.CalculateAmounts();
        _logger.Debug($"GetInvoiceById with data: {invoice.ToString()}");

        return _mapper.Map<InvoiceDto>(invoice);
    }

}