using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Invoices.Queries;
public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, ICollection<InvoiceDto>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetInvoicesQueryHandler(IInvoiceRepository invoiceRepository,
                                IMapper mapper,
                                ICustomLogger logger)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ICollection<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceRepository.ListAsync(c => c.Id != Guid.Empty, cancellationToken, true, new string[] { "InvoiceLines" });
        invoices.ToList()
                .ForEach(c => c.CalculateAmounts());
        var invoicesDto = _mapper.Map<List<InvoiceDto>>(invoices);
        _logger.Debug($"GetInvoices count: {invoicesDto.Count}");

        return invoicesDto;
    }
}