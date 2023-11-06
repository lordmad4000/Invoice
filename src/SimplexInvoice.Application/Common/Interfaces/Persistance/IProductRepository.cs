using SimplexInvoice.Application.Common.Models;
using SimplexInvoice.Domain.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<IEnumerable<BasicProduct>> GetBasicProductsContainsName(string name, CancellationToken cancellationToken, bool tracking = false);
    }
}