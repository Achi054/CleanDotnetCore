using System.Collections.Generic;

namespace IdentityServer.Contracts.V1.ResponseModel
{
    public class AuthFailureResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
