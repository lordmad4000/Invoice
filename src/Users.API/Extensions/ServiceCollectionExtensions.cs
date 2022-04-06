﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Infra.Repositories;
using Users.Domain.Interfaces;
using Users.Infra.Data;
using Users.Application.Services;
using Users.Application.Interfaces;

namespace Users.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();

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

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserService, UserService>();
        }
    }
}