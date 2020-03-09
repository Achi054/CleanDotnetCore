using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapperRegister;
using DependencyRegister;
using IdentityServer.Contracts.V1.RequestModel;
using IdentityServer.Contracts.V1.ResponseModel;
using IdentityServer.Data;
using MediatrRegister;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrderApi.Models;
using ValidationRegister;
using IdentityRoute = IdentityServer.Contracts.V1;
using OrderRoutes = OrderApi.Contracts.V1;

namespace OrderApi.IntegrationTest
{
    public abstract class IntegrationTestBase : IDisposable
    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider serviceProvider;

        protected IntegrationTestBase()
        {
            var webappFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(configuration =>
                {
                    configuration.ConfigureServices(configureServices =>
                    {
                        configureServices.RemoveAll(typeof(UserDbContext));

                        configureServices.AddDbContext<UserDbContext>(optionsAction =>
                        {
                            optionsAction.UseInMemoryDatabase("TestDb");
                        });

                        configureServices.AddMappers(typeof(MapperConfiguratorAttribute));

                        configureServices.AddApplicationDependency();

                        configureServices.AddMediater();

                        configureServices.AddValidators();
                    });
                });

            serviceProvider = webappFactory.Services;
            TestClient = webappFactory.CreateClient();
        }

        public void Dispose()
        {
            using var serviceScope = serviceProvider.CreateScope();
            var userDbContext = serviceScope.ServiceProvider.GetService<UserDbContext>();
            userDbContext.Database.EnsureDeleted();
        }

        protected async Task AuthenticateAsync()
         => TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtToken());

        protected async Task<OrderDetails> CreateAsync(OrderDetails order)
        {
            var response = await TestClient.PostAsJsonAsync(OrderRoutes.ApiRoutes.Order.Create, order);

            return await response.Content.ReadAsAsync<OrderDetails>();
        }

        private async Task<string> GetJwtToken()
        {
            var response = await TestClient.PostAsJsonAsync(IdentityRoute.ApiRoutes.Identity.Register, new UserRegistrationRequest { Email = "test@test.com", Name = "TestUser", Password = "Test@123" });

            var content = response.Content.ReadAsAsync<AuthSuccessResponse>();

            return content.Result.Token;
        }
    }
}
