using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Models;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Infra.Data;
using SimplexInvoice.Infra.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace SimplexInvoice.Infra.Repositories
{
    public class ProductRepository : CachedRepositoryDecorator<Product>, IProductRepository
    {
        public ProductRepository(EFContext context, 
                                 ICacheService cacheService, 
                                 IRepository<Product> repository,
                                 ICustomLogger logger)
            : base(context, cacheService, repository, logger)
        {
        }

        public async Task<IEnumerable<BasicProduct>> GetBasicProductsContainsName(string name, CancellationToken cancellationToken, bool tracking = false)
        {
            var cacheKey = $"{_cacheKey}-GetBasicProductsContainsName-{name}";

            if (!TryGetCache(cacheKey, out List<BasicProduct> results))
            {
                try
                {
                    var query = _context.Product.AsQueryable();
                                                
                    if (!tracking)
                        query = query.AsNoTracking();
                    
                    results = await query.Where(c => c.Name.ToLower().Contains(name.ToLower()))
                                         .Include(c => c.TaxRate)
                                         .Select(c => new BasicProduct
                                          {
                                              Id = c.Id,
                                              Code = c.Code,
                                              Name = c.Name,
                                              Price = c.UnitPrice.Amount,
                                              Currency = c.UnitPrice.Currency,
                                              TaxRateValue = c.TaxRate.Value
                                          })
                                         .ToListAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new DataBaseException(ex.InnerException.Message);
                }
                TrySetCache(cacheKey, results);
            }

            return results;
        }

    }
}