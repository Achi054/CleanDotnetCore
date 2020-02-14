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
                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(security, new string[0]);

                opts.AddSecurityDefinition("Bearer", security);
                opts.AddSecurityRequirement(securityRequirement);
            });
        }
    }
}
