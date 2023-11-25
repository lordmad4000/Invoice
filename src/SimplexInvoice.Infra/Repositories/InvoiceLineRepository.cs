using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Infra.Data;

namespace SimplexInvoice.Infra.Repositories
{
    public class InvoiceLineRepository : CachedRepositoryDecorator<InvoiceLine>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(EFContext context, 
                                     ICacheService cacheService, 
                                     IRepository<InvoiceLine> repository,
                                     ICustomLogger logger)
            : base(context, cacheService, repository, logger)
        {
        }

    }
}