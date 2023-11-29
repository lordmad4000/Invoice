using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.TaxRates.Exceptions;
using SimplexInvoice.Application.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.TaxRates.Commands;
public class TaxRateUpdateHandler : IRequestHandler<TaxRateUpdateCommand, TaxRateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaxRateRepository _taxRateRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public TaxRateUpdateHandler(IUnitOfWork unitOfWork,
                                ITaxRateRepository taxRateRepository,
                                IMapper mapper,
                                ICustomLogger logger)
    {
        _unitOfWork = unitOfWork;
        _taxRateRepository = taxRateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TaxRateDto> Handle(TaxRateUpdateCommand request, CancellationToken cancellationToken)
    {
        var taxRate = await _taxRateRepository.GetAsync(c => c.Id == request.Id, cancellationToken, false)
            ?? throw new NotFoundException("TaxRate not found.");

        taxRate.Update(request.Name, request.Value);
        TaxRateDto taxRateDto = _mapper.Map<TaxRateDto>(await _taxRateRepository.UpdateAsync(taxRate, cancellationToken));
        if (await _unitOfWork.SaveChangesAsync(cancellationToken) == 0)
            throw new TaxRateUpdatingException($"Error updating the TaxRate.");

        _logger.Debug(@$"TaxRate Updated successfully with data: {taxRate}");

        return taxRateDto;
    }

}