using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Contracts.V1.RequestModel
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
