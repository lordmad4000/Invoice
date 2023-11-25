using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class TaxRateRepository : CachedRepositoryDecorator<TaxRate>, ITaxRateRepository
    {
        public TaxRateRepository(EFContext context, 
                                 ICacheService cacheService, 
                                 IRepository<TaxRate> repository,
                                 ICustomLogger logger)
            : base(context, cacheService, repository, logger)
        {
        }

    }
}