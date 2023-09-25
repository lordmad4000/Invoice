using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Application.Services;
using System.Linq;
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
            services.AddScoped<ApplicationTestService>();            

            return services;            
        }        
    }
}