using System.Threading.Tasks;
using IdentityServer.Contracts.V1;
using IdentityServer.Contracts.V1.RequestModel;
using IdentityServer.Contracts.V1.ResponseModel;
using Refit;

namespace OrderSdk
{
    public interface IIdentityApi
    {
        [Post(ApiRoutes.Identity.Login)]
        Task<ApiResponse<AuthSuccessResponse>> Login([Body] UserLoginRequest userLoginRequest);

        [Post(ApiRoutes.Identity.Register)]
        Task<ApiResponse<AuthSuccessResponse>> Register([Body] UserRegistrationRequest userRegistrationRequest);

        [Post(ApiRoutes.Identity.Refresh)]
        Task<ApiResponse<AuthSuccessResponse>> Refresh([Body] RefreshTokenRequest refreshTokenRequest);
    }
}
