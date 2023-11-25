using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class InvoiceRepository : CachedRepositoryDecorator<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(EFContext context,
                                 ICacheService cacheService, 
                                 IRepository<Invoice> repository,
                                 ICustomLogger logger)
            : base(context, cacheService, repository, logger)
        {
        }

    }
}