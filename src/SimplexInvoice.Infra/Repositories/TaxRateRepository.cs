using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class TaxRateRepository : RepositoryBase<TaxRate>, ITaxRateRepository
    {
        private readonly EFContext _context;
        public TaxRateRepository(IUnitOfWork unitOfWork, ICacheService cacheService) : base(unitOfWork, cacheService)
        {
            _context = unitOfWork.GetContext();
        }

    }
}