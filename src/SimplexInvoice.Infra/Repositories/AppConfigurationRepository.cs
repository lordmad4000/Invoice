using Microsoft.EntityFrameworkCore;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Entities;
using SimplexInvoice.Infra.Data;
using SimplexInvoice.Infra.Exceptions;
using System.Threading.Tasks;
using System;

namespace SimplexInvoice.Infra.Repositories
{
    public class AppConfigurationRepository : IAppConfigurationRepository
    {
        private Guid defaultId = Guid.Parse("93cb9570-7f16-4c26-aafc-3b96b2bba055");
        private readonly DbSet<AppConfiguration> _dbSet;
        private readonly IUnitOfWork _unitOfWork;

        public AppConfigurationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.GetContext().Set<AppConfiguration>();
        }

        public async Task<AppConfiguration> AddAsync(AppConfiguration appConfiguration)
        {
            appConfiguration.Id = defaultId;
            await _dbSet.AddAsync(appConfiguration);

            return appConfiguration;
        }
        
        public void Update(AppConfiguration appConfiguration)
        {
            _dbSet.Update(appConfiguration);
        }

        public async Task<AppConfiguration> GetAsync()
        {
            try
            {
                var result = await _dbSet.AsNoTracking()
                                         .FirstOrDefaultAsync(c => c.Id == defaultId);

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.InnerException.Message);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

    }
}