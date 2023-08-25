using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly EFContext _context;
        public ProductRepository(IUnitOfWork unitOfWork, ICacheService cacheService) : base(unitOfWork, cacheService)
        {
            _context = unitOfWork.GetContext();
        }

    }
}