using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SimplexInvoice.Application
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