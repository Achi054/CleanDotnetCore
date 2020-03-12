using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers
{
    public class SecretController : ControllerBase
    {
        [HttpGet("secret")]
        public IActionResult Get() => Ok("I have no secret !!");
    }
}
