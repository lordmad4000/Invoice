using System;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}