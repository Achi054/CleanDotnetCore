using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApiResources()
            => new[] { new ApiResource("ApiOne") };

        public static IEnumerable<Client> GetClients()
            => new[] {
                new Client
                {
                    ClientId = "054",
                    ClientSecrets = new[] { new Secret("sujith_acharya".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = new[] { "ApiOne" }
                }
            };
    }
}
