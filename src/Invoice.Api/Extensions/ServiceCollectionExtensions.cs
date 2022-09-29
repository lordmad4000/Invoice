using Invoice.Api.AutoMapper;
using Invoice.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Invoice.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Invoice.Api", Description = "Invoice Core Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Please insert JWT with Bearer into field."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DtoToResponseMappingProfile), 
                                   typeof(RequestToCommandMappingProfile),
                                   typeof(EntityDtoMappingProfile));
            AutoMapperConfig.RegisterMappings();

            return services;
        }

    }
}