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
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly EFContext _context;
        public CustomerRepository(IUnitOfWork unitOfWork, ICacheService cacheService) : base(unitOfWork, cacheService)
        {
            _context = unitOfWork.GetContext();
        }

        public async Task<IEnumerable<BasicCustomer>> GetBasicCustomerContainsFullName(string fullName, CancellationToken cancellationToken, bool tracking = false)
        {
            var cacheKey = $"{_cacheKey}-GetBasicCustomerContainsFullName-{fullName}";

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

        public async Task<IEnumerable<BasicCustomer>> GetBasicCustomerContainsEmail(string email, CancellationToken cancellationToken, bool tracking = false)
        {
            var cacheKey = $"{_cacheKey}-GetBasicCustomerContainsEmail-{email}";

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

        public async Task<IEnumerable<BasicCustomer>> GetBasicCustomerContainsIdDocumentNumber(string idDocumentNumber, CancellationToken cancellationToken, bool tracking = false)
        {
            var cacheKey = $"{_cacheKey}-GetBasicCustomerContainsIdDocumentNumber-{idDocumentNumber}";

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