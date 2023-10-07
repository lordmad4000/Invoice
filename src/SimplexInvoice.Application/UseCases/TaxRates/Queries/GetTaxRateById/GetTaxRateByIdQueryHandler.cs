using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System.Threading.Tasks;
using System.Threading;
using SimplexInvoice.Application.Common.Exceptions;

namespace SimplexInvoice.Application.TaxRates.Queries;
public class GetTaxRateByIdQueryHandler : IRequestHandler<GetTaxRateByIdQuery, TaxRateDto>
{
    private readonly ITaxRateRepository _taxRateRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetTaxRateByIdQueryHandler(ITaxRateRepository taxRateRepository,
                                      IMapper mapper,
                                      ICustomLogger logger)
    {
        _taxRateRepository = taxRateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TaxRateDto> Handle(GetTaxRateByIdQuery request, CancellationToken cancellationToken)
    {
        var taxRate = await _taxRateRepository.GetAsync(c => c.Id == request.Id, cancellationToken, true, $"Id=={request.Id}");
        if (taxRate is null)
            throw new NotFoundException($"TaxRate with id {request.Id} was not found");

        _logger.Debug($"GetTaxRateById with data: {taxRate.ToString()}");

        return _mapper.Map<TaxRateDto>(taxRate);
    }
}