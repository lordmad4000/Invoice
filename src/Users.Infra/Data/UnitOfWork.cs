using System;
using System.Threading.Tasks;
using Users.Domain.Interfaces;

namespace Users.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _dbContext;

        public UnitOfWork(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}