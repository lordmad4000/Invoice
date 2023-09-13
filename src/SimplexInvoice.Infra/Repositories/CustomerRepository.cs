using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly EFContext _context;
        public CustomerRepository(IUnitOfWork unitOfWork, ICacheService cacheService) : base(unitOfWork, cacheService)
        {
            _context = unitOfWork.GetContext();
        }

    }
}