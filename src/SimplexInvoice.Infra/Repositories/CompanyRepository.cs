using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class CompanyRepository : CachedRepositoryDecorator<Company>, ICompanyRepository
    {
        public CompanyRepository(EFContext context, 
                                 ICacheService cacheService, 
                                 IRepository<Company> repository,
                                 ICustomLogger logger)
            : base(context, cacheService, repository, logger)
        {
        }

    }
}