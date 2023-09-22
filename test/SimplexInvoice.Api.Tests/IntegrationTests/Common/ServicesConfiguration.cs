using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.AutoMapper;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Tests.IntegrationTests.Common;
using SimplexInvoice.Application.AutoMapper;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Application.Services;
using SimplexInvoice.Domain.Interfaces;
using SimplexInvoice.Infra.Configuration;
using SimplexInvoice.Infra.Data;
using SimplexInvoice.Infra.Repositories;
using SimplexInvoice.Infra.Services;

namespace SimplexInvoice.SimplexInvoice.Api;

public static class ServicesConfiguration
{
    public static IServiceCollection BuildDependencies()
    {
        IServiceCollection services = new ServiceCollection();       
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                     .AddJsonFile("appsettings.json")
                                                                     .Build();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddAutoMapper(typeof(RequestToCommandMappingProfile),
                               typeof(RequestToDtoMappingProfile),
                               typeof(ResponseToDtoMappingProfile),
                               typeof(EntityToDtoMappingProfile),
                               typeof(DtoToCommandMappingProfile));
        AutoMapperConfig.RegisterMappings();        
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<EFContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IIdDocumentTypeRepository, IdDocumentTypeRepository>();
        services.AddScoped<IInvoiceLineRepository, InvoiceLineRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITaxRateRepository, TaxRateRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordEncryption, PasswordEncryptService>();
        services.AddScoped<ITokenService, JWTokenService>();
        services.AddScoped<IInfraTestService, InfraTestService>();
        services.AddSingleton<ICustomLogger, TestsLogger>();
        services.AddScoped<IValidatorService, ValidatorService>();
        services.AddScoped<IPasswordService, UserPasswordService>();
        services.AddScoped<ApplicationTestService>();
        services.Configure<CacheConfig>(configuration.GetSection("CacheConfig"));
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();
        services.AddTransient<IdDocumentTypesController>();
        services.AddTransient<TaxRatesController>();
        services.AddTransient<CustomersController>();
        services.AddTransient<CompaniesController>();
        services.AddTransient<ProductsController>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ValidatorService).Assembly));

        return services;                     
    }

}