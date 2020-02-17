﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Contracts.V1.RequestModel
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
