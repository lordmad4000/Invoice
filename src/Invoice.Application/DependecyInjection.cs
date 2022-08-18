using Invoice.Application.Interfaces;
using Invoice.Application.Services;
using Invoice.Application.Services.Configuration.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidatorService, ValidatorService>();
            services.AddScoped<IPasswordService, UserPasswordService>();
            services.AddScoped<ITokenService, JWTokenService>();
            services.AddScoped<IIdDocumentTypeCommandService, IdDocumentTypeCommandService>();

            return services;            
        }        
    }
}