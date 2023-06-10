using Invoice.Application.Interfaces;
using Invoice.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Invoice.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IValidatorService, ValidatorService>();
            services.AddScoped<IPasswordService, UserPasswordService>();

            return services;            
        }        
    }
}