using System.Text;
using IdentityServer.AppSettings;
using IdentityServer.Data;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SecurityRegister;

namespace IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("IdentityConnectionString");
            services.AddDbContext<UserDbContext>(optionsAction => optionsAction.UseSqlServer(connectionString));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<UserDbContext>();

            services.AddScoped<IIdentityService, IdentityService>();

            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenParameters);

            services.AddAuthentication(configureOptions =>
                {
                    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.SaveToken = true;
                    configureOptions.TokenValidationParameters = tokenParameters;
                });

            services.AddSwagger<Startup>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonEndpoint);
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint(swaggerOptions.UriEndpoint, swaggerOptions.Description);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
