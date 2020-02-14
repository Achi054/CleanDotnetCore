using System.Threading.Tasks;
using IdentityServer.Contracts.V1;
using IdentityServer.Contracts.V1.RequestModel;
using IdentityServer.Contracts.V1.ResponseModel;
using IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers.V1
{
    [ApiController]
    public class IdentityApiController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityApiController(IIdentityService identityService) => _identityService = identityService;

        [HttpPost(ApiRoutes.Identity.Login)]
        public IActionResult Login() => Ok();

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var identityResponse = await _identityService.RegisterAsync(request.Name, request.Email, request.Password);

            if (!identityResponse.Success)
                return BadRequest(new AuthFailureResponse { Errors = identityResponse.Errors });

            return Ok(new AuthSuccessResponse { Token = identityResponse.Token });
        }
    }
}
