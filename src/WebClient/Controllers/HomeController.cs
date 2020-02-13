using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _client;

        public HomeController(IHttpClientFactory client) => _client = client;

        public async Task<IActionResult> Index()
        {
            var orderApiClient = _client.CreateClient("OrderApi");

            var response = await orderApiClient.GetAsync("/api/order");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return View(new
                {
                    model = JsonConvert.DeserializeObject<object>(content)
                });
            }

            return View(
                new
                {
                    model = "No Content"
                });
        }
    }
}
