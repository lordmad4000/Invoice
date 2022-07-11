using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.Interfaces;
using Invoice.Infra.Repositories;
using Invoice.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordEncryption, PasswordEncryptService>();

            return services;            
        }        
    }
}