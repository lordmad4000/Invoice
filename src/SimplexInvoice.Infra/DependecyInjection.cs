using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Domain.Interfaces;
using SimplexInvoice.Infra.Configuration;
using SimplexInvoice.Infra.Data;
using SimplexInvoice.Infra.Repositories;
using SimplexInvoice.Infra.Services;
using System.Text;
using System;

namespace SimplexInvoice.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIdDocumentTypeRepository, IdDocumentTypeRepository>();
            services.AddScoped<IPasswordEncryption, PasswordEncryptService>();
            services.AddScoped<ITokenService, JWTokenService>();
            services.AddScoped<IInfraTestService, InfraTestService>();
            services.AddSingleton<ICustomLogger, LoggerSeriLog>();

            return services;            
        }        

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            return services.AddDbContext<EFContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }        

        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheConfig>(configuration.GetSection("CacheConfig"));            
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTConfig>(configuration.GetSection(JWTConfig.JWT));
            var jwtConfig = configuration.GetSection(JWTConfig.JWT).Get<JWTConfig>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.SecretKey);
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(c =>
             {
                 c.RequireHttpsMetadata = false;
                 c.SaveToken = true;
                 c.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ClockSkew = TimeSpan.Zero
                 };
             });

            return services;
        }        

    }
}