using SimplexInvoice.Domain.Invoices;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IInvoiceLineRepository : ICacheableRepository<InvoiceLine>
    {
    }
}