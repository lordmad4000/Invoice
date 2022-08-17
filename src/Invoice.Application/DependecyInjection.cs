using Invoice.Application.Interfaces;
using Invoice.Application.Services;
using Invoice.Application.Services.Configuration.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IValidatorService, ValidatorService>();
            services.AddScoped<IPasswordService, UserPasswordService>();
            services.AddScoped<ITokenService, JWTokenService>();

            services.AddScoped<IIdDocumentTypeCommandService, IdDocumentTypeCommandService>();


            return services;            
        }        
    }
}