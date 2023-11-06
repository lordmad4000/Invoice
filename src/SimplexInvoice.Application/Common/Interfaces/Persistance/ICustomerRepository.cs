using SimplexInvoice.Application.Common.Models;
using SimplexInvoice.Domain.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
        Task<IEnumerable<BasicCustomer>> GetBasicCustomersContainsFullName(string fullName, CancellationToken cancellationToken, bool tracking = false);
        Task<IEnumerable<BasicCustomer>> GetBasicCustomersContainsEmail(string email, CancellationToken cancellationToken, bool tracking = false);
        Task<IEnumerable<BasicCustomer>> GetBasicCustomersContainsIdDocumentNumber(string idDocumentNumber, CancellationToken cancellationToken, bool tracking = false);
    }
}