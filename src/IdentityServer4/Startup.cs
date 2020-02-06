using IdentityServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("AppDb"));

            services
                .AddIdentity<IdentityUser, IdentityRole>(option =>
                {
                    option.Password.RequireDigit = true;
                    option.Password.RequiredLength = 5;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(configure =>
            {
                configure.Cookie.Name = "Identity.Cookie";
                configure.LoginPath = "/Home/Login";
            });

            services.AddIdentityServer()
                    .AddAspNetIdentity<IdentityUser>()
                    .AddInMemoryApiResources(Configuration.GetApiResources())
                    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
                    .AddInMemoryClients(Configuration.GetClients())
                    .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
