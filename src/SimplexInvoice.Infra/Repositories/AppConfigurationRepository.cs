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
        private Guid defaultId = new AppConfiguration().Id;
        private readonly EFContext _context;

        public AppConfigurationRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<AppConfiguration> AddAsync(AppConfiguration appConfiguration)
        {
            appConfiguration.Id = defaultId;
            await _context.AppConfiguration.AddAsync(appConfiguration);

            return appConfiguration;
        }
        
        public void Update(AppConfiguration appConfiguration)
        {
            _context.AppConfiguration.Update(appConfiguration);
        }

        public async Task<AppConfiguration> GetAsync()
        {
            try
            {
                var result = await _context.AppConfiguration.AsNoTracking()
                                                            .FirstOrDefaultAsync(c => c.Id == defaultId);

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.InnerException.Message);
            }
        }

    }
}