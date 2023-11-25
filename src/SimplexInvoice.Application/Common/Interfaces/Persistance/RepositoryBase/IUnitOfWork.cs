using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}