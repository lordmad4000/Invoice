using System;
using System.Threading.Tasks;
using Users.Domain.Base;
using Users.Domain.Interfaces;

namespace TestUsers.API.Mock
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        public IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity
        {
            throw new System.NotImplementedException();
        }


        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}