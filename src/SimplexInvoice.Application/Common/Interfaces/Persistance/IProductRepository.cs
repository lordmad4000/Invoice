using SimplexInvoice.Domain.Products;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
    }
}