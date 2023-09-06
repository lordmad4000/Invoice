using MediatR;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.TaxRates.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.TaxRates.Commands;

public class TaxRateRemoveHandler : IRequestHandler<TaxRateRemoveCommand, bool>
{
    private readonly ITaxRateRepository _taxRateRepository;
    private readonly ICustomLogger _logger;

    public TaxRateRemoveHandler(ITaxRateRepository taxRateRepository,
                                ICustomLogger logger)
    {
        _taxRateRepository = taxRateRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(TaxRateRemoveCommand request, CancellationToken cancellationToken)
    {
        await _taxRateRepository.DeleteAsync(request.Id, cancellationToken);
        if (await _taxRateRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new TaxRateRemovingException($"Error removing the TaxRate.");

        _logger.Debug(@$"TaxRate with id {request.Id} removed.");

        return true;
    }
}