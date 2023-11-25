using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.TaxRates.Exceptions;
using SimplexInvoice.Domain.TaxRates;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.TaxRates.Commands;
public class TaxRateRegisterHandler : IRequestHandler<TaxRateRegisterCommand, TaxRateDto>
{
    private readonly ITaxRateRepository _taxrateRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    public TaxRateRegisterHandler(ITaxRateRepository taxrateRepository, 
                                  IMapper mapper, 
                                  ICustomLogger logger)
    {
        _taxrateRepository = taxrateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TaxRateDto> Handle(TaxRateRegisterCommand request, CancellationToken cancellationToken)
    {
        TaxRate taxrate = TaxRate.Create(request.Name, 
                                         request.Value);
        TaxRateDto taxrateDto = _mapper.Map<TaxRateDto>(await _taxrateRepository.AddAsync(taxrate, cancellationToken));
        //if (await _taxrateRepository.SaveChangesAsync(cancellationToken) == 0)
        //    throw new TaxRateRegisteringException($"Error registering the TaxRate.");

        _logger.Debug(@$"TaxRate Registered successfully with data: {taxrate}");

        return taxrateDto;
    }
}