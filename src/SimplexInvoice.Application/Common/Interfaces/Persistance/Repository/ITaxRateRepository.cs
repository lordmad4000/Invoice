using SimplexInvoice.Domain.TaxRates;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface ITaxRateRepository : ICacheableRepository<TaxRate>
    {
    }
}