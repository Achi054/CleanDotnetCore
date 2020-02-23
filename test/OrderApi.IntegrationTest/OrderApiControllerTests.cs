using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using OrderApi.Models;
using Xunit;

using OrderApiRoutes = OrderApi.Contracts.V1;

namespace OrderApi.IntegrationTest
{
    public class OrderApiControllerTests : IntegrationTestBase
    {
        [Fact]
        public async Task Get_WhenNoDataExists_ReturnEmpty()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync(OrderApiRoutes.ApiRoutes.Order.Get);
            var content = response.Content.ReadAsAsync<IEnumerable<OrderDetails>>().Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Get_WhenPassedWithValid_ReturnOrderDetails()
        {
            await AuthenticateAsync();
            var orderDetails = await CreateAsync(new OrderDetails { Id = 1, Name = "Test", Quantity = 3 });

            var response = await TestClient.GetAsync(OrderApiRoutes.ApiRoutes.Order.GetById.Replace("{id}", orderDetails.Id.ToString()));
            var content = response.Content.ReadAsAsync<OrderDetails>().Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().NotBeNull();
            content.Id.Should().Be(1);
            content.Name.Should().Be("Test");
            content.Quantity.Should().Be(3);
        }
    }
}
