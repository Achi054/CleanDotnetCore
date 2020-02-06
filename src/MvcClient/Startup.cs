using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(configureOptions =>
                    {
                        configureOptions.DefaultScheme = "authCookie";
                        configureOptions.DefaultChallengeScheme = "oidc";
                    })
                    .AddCookie("authCookie")
                    .AddOpenIdConnect("oidc", configureOptions =>
                    {
                        configureOptions.Authority = "https://localhost:44326/";

                        configureOptions.ClientId = "054_mvc";
                        configureOptions.ClientSecret = "sujith_acharya_mvc";

                        configureOptions.SaveTokens = true;

                        configureOptions.ResponseType = "code";
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
