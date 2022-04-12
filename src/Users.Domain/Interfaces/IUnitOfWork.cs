using System;
using System.Threading.Tasks;
using Users.Domain.Base;

namespace Users.Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync();
        IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity;
    }
}