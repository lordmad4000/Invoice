using System;
using System.Threading.Tasks;

namespace Invoice.Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}