﻿namespace IdentityServer.Contracts.V1.ResponseModel
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
