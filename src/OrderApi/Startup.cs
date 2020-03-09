using AutoMapperRegister;
using DependencyRegister;
using MediatrRegister;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderApi.Options;
using Repository.EntityFramework.Context;
using SecurityRegister;
using ValidationRegister;

namespace OrderApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddSwagger();

            services.AddControllers();

            services.AddDbContext<eComContext>(option => option.UseInMemoryDatabase("eCommerce"));

            services.AddMappers(typeof(MapperConfiguratorAttribute));

            services.AddApplicationDependency();

            services.AddMediater();

            services.AddValidators();

            services.AddCosmosDb(Configuration);

            services.AddAuthorization(configure =>
            {
                configure.AddPolicy("CanDelete", configurePolicy => configurePolicy.RequireClaim("order.delete", "true"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonEndpoint);
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint(swaggerOptions.UriEndpoint, swaggerOptions.Description);
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
