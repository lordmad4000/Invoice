using System;
using System.Threading.Tasks;

namespace Users.Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}