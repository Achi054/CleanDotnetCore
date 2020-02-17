namespace IdentityServer.Contracts.V1.RequestModel
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
