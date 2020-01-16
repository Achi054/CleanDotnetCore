using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderApiController : ControllerBase
    {
        private readonly ILogger<OrderApiController> _logger;

        public OrderApiController(ILogger<OrderApiController> logger)
        {
            _logger = logger;
        }
    }
}
