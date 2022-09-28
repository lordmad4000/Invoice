using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.Interfaces;
using Invoice.Infra.Data;
using Invoice.Infra.Interfaces;
using Invoice.Infra.Repositories;
using Invoice.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIdDocumentTypeRepository, IdDocumentTypeRepository>();
            services.AddScoped<IPasswordEncryption, PasswordEncryptService>();
            services.AddSingleton<ICustomLogger>(s => new LoggerSeriLog(configuration));

            return services;            
        }        

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<EFContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection")));
        }        

        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddSingleton<ICacheService, MemoryCacheService>();

            return services;
        }

    }
}