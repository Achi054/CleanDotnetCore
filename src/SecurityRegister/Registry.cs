using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace SecurityRegister
{
    public static class Registry
    {
        public static void AddSwagger<T>(this IServiceCollection services) where T : class
        {
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerce API", Version = "V1" });

                opts.ExampleFilters();

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

                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            services.AddSwaggerExamplesFromAssemblyOf<T>();
        }
    }
}
