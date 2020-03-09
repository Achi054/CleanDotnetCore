using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SecurityRegister
{
    public static class Registry
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerce API", Version = "V1" });

                var security = new OpenApiSecurityScheme
                {
                    Description = "Jwt Authrization Header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                opts.AddSecurityDefinition("Bearer", security);

                var securityRequirement = new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            }
                        }, Array.Empty<string>()
                    }
                };
                opts.AddSecurityRequirement(securityRequirement);
            });
        }
    }
}
