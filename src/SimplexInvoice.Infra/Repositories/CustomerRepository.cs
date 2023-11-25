using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Common.Models;
using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Infra.Data;
using SimplexInvoice.Infra.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Infra.Repositories
{
    public class CustomerRepository : CachedRepositoryDecorator<Customer>, ICustomerRepository
    {
        public CustomerRepository(EFContext context, 
                                  ICacheService cacheService, 
                                  IRepository<Customer> repository,
                                  ICustomLogger logger)
            : base(context, cacheService, repository, logger)
        {
        }

        public async Task<IEnumerable<BasicCustomer>> GetBasicCustomersContainsFullName(string fullName, CancellationToken cancellationToken, bool tracking = false)
        {
            var cacheKey = $"{_cacheKey}-GetBasicCustomersContainsFullName-{fullName}";

            if (!TryGetCache(cacheKey, out List<BasicCustomer> results))
            {
                try
                {
                    var query = _context.Customer.AsQueryable();
                    if (!tracking)
                        query = query.AsNoTracking();

                    results = await query.Where(c => c.FirstName.ToLower().Contains(fullName.ToLower()) || c.LastName.ToLower().Contains(fullName.ToLower()))
                                         .Select(c => new BasicCustomer
                                         {
                                             Id = c.Id,
                                             FullName = $"{c.FirstName} {c.LastName}",
                                             IdDocumentNumber = c.IdDocumentNumber,
                                             Phone = c.Phone.Phone,
                                             Email = c.EmailAddress.Address,
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

        public async Task<IEnumerable<BasicCustomer>> GetBasicCustomersContainsEmail(string email, CancellationToken cancellationToken, bool tracking = false)
        {
            var cacheKey = $"{_cacheKey}-GetBasicCustomersContainsEmail-{email}";

            if (!TryGetCache(cacheKey, out List<BasicCustomer> results))
            {
                try
                {
                    var query = _context.Customer.AsQueryable();
                    if (!tracking)
                        query = query.AsNoTracking();

                    results = await query.Where(c => c.EmailAddress.Address.ToLower().Contains(email.ToLower()))
                                         .Select(c => new BasicCustomer
                                         {
                                             Id = c.Id,
                                             FullName = $"{c.FirstName} {c.LastName}",
                                             IdDocumentNumber = c.IdDocumentNumber,
                                             Phone = c.Phone.Phone,
                                             Email = c.EmailAddress.Address,
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

        public async Task<IEnumerable<BasicCustomer>> GetBasicCustomersContainsIdDocumentNumber(string idDocumentNumber, CancellationToken cancellationToken, bool tracking = false)
        {
            var cacheKey = $"{_cacheKey}-GetBasicCustomersContainsIdDocumentNumber-{idDocumentNumber}";

            if (!TryGetCache(cacheKey, out List<BasicCustomer> results))
            {
                try
                {
                    var query = _context.Customer.AsQueryable();
                    if (!tracking)
                        query = query.AsNoTracking();

                    results = await query.Where(c => c.IdDocumentNumber.ToLower().StartsWith(idDocumentNumber.ToLower()))
                                         .Select(c => new BasicCustomer
                                         {
                                             Id = c.Id,
                                             FullName = $"{c.FirstName} {c.LastName}",
                                             IdDocumentNumber = c.IdDocumentNumber,
                                             Phone = c.Phone.Phone,
                                             Email = c.EmailAddress.Address,
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