using System;
using System.Threading.Tasks;
using SimplexInvoice.Infra.Exceptions;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System.Threading;

namespace SimplexInvoice.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _dbContext;

        public UnitOfWork(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
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