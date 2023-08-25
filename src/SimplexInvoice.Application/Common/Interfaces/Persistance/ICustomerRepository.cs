using SimplexInvoice.Domain.Customers;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
    }
}