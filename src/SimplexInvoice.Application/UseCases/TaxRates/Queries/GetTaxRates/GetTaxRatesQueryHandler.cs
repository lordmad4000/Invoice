using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.TaxRates.Queries;
public class GetTaxRatesQueryHandler : IRequestHandler<GetTaxRatesQuery, ICollection<TaxRateDto>>
{
    private readonly ITaxRateRepository _taxRateRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetTaxRatesQueryHandler(ITaxRateRepository taxRateRepository,
                                IMapper mapper,
                                ICustomLogger logger)
    {
        _taxRateRepository = taxRateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ICollection<TaxRateDto>> Handle(GetTaxRatesQuery request, CancellationToken cancellationToken)
    {
        var taxRates = await _taxRateRepository.ListAsync(c => c.Id != Guid.Empty, cancellationToken);
        var taxRatesDto = _mapper.Map<List<TaxRateDto>>(taxRates);
        _logger.Debug($"GetTaxRates count: {taxRatesDto.Count}");

        return taxRatesDto;
    }
}