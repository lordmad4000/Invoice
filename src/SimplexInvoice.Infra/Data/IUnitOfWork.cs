using System;
using System.Threading.Tasks;

namespace SimplexInvoice.Infra.Data
{
    public interface IUnitOfWork: IDisposable
    {
        EFContext GetContext();
        Task<int> SaveChangesAsync();
    }
}