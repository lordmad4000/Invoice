using SimplexInvoice.Domain.Entities;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Common.Interfaces.Persistance
{
    public interface IAppConfigurationRepository
    {
        Task<AppConfiguration> AddAsync(AppConfiguration appConfiguration);
        void Update(AppConfiguration appConfiguration);
        Task<AppConfiguration> GetAsync();
    }
}