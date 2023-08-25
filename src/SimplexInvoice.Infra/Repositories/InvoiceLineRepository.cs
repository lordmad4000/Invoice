using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class InvoiceLineRepository : RepositoryBase<InvoiceLine>, IInvoiceLineRepository
    {
        private readonly EFContext _context;
        public InvoiceLineRepository(IUnitOfWork unitOfWork, ICacheService cacheService) : base(unitOfWork, cacheService)
        {
            _context = unitOfWork.GetContext();
        }

    }
}