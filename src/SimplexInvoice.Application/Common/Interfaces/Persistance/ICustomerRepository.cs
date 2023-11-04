using SimplexInvoice.Application.Common.Models;
using SimplexInvoice.Domain.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
        Task<IEnumerable<BasicCustomer>> GetBasicCustomerContainsFullName(string fullName, CancellationToken cancellationToken, bool tracking = false);
        Task<IEnumerable<BasicCustomer>> GetBasicCustomerContainsEmail(string email, CancellationToken cancellationToken, bool tracking = false);
        Task<IEnumerable<BasicCustomer>> GetBasicCustomerContainsIdDocumentNumber(string idDocumentNumber, CancellationToken cancellationToken, bool tracking = false);
    }
}