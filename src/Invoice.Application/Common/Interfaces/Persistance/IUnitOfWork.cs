using System;
using System.Threading.Tasks;

namespace Invoice.Application.Common.Interfaces.Persistance
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}