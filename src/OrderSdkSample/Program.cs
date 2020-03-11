using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer.Contracts.V1.RequestModel;
using OrderSdk;
using Refit;

namespace OrderSdkSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;

            var identityApiClient = new HttpClient { BaseAddress = new Uri("https://localhost:44399/") };
            var identityApi = RestService.For<IIdentityApi>(identityApiClient);

            var registerResponse = await identityApi.Register(new UserRegistrationRequest
            {
                Email = "test@test.com",
                Password = "test123",
                Name = "Test User"
            });

            cachedToken = registerResponse.Content.Token;

            Console.WriteLine($"Token: {registerResponse.Content.Token}");

            var loginResponse = await identityApi.Login(new UserLoginRequest
            {
                Email = "test@test.com",
                Password = "test123"
            });

            Console.WriteLine(loginResponse.IsSuccessStatusCode ? "Login Successfull" : "Login Unsuccessful");

            var orderApiClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };
            var orderApi = RestService.For<IOrderApi>(orderApiClient, new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });

            var getOrderResponse = await orderApi.Get();

            Console.WriteLine("Order Details:");
            foreach (var item in getOrderResponse.Content)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Quantity);
            }
        }
    }
}
