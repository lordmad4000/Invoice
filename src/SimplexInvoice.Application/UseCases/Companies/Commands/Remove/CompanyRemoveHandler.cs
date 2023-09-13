using MediatR;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Companies.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Companies.Commands;

public class CompanyRemoveHandler : IRequestHandler<CompanyRemoveCommand, bool>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICustomLogger _logger;

    public CompanyRemoveHandler(ICompanyRepository companyRepository,
                               ICustomLogger logger)
    {
        _companyRepository = companyRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(CompanyRemoveCommand request, CancellationToken cancellationToken)
    {
        await _companyRepository.DeleteAsync(request.Id, cancellationToken);
        if (await _companyRepository.SaveChangesAsync(cancellationToken) == 0)
            throw new CompanyRemovingException($"Error removing the Company.");

        _logger.Debug(@$"Company with id {request.Id} removed.");

        return true;
    }
}