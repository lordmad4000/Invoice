using System;
using System.Threading.Tasks;
using SimplexInvoice.Infra.Exceptions;

namespace SimplexInvoice.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _dbContext;

        public UnitOfWork(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EFContext GetContext() =>
            _dbContext;

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.InnerException.Message);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}