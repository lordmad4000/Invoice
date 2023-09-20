using Microsoft.OpenApi.Models;
using SimplexInvoice.Api.AutoMapper;
using SimplexInvoice.Application.AutoMapper;

namespace SimplexInvoice.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimplexInvoice.Api", Description = "SimplexInvoice Core Api", Version = "v1" });

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
            services.AddAutoMapper(typeof(RequestToCommandMappingProfile),
                                   typeof(RequestToDtoMappingProfile),
                                   typeof(ResponseToDtoMappingProfile),
                                   typeof(EntityToDtoMappingProfile),
                                   typeof(DtoToCommandMappingProfile));
            AutoMapperConfig.RegisterMappings();

            return services;
        }

    }
}