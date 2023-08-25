using SimplexInvoice.Domain.Companies;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface ICompanyRepository : IAsyncRepository<Company>
    {
    }
}