using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> GetApiResources()
            => new[] { new ApiResource("ApiOne"), new ApiResource("ApiTwo") };

        public static IEnumerable<Client> GetClients()
            => new[] {
                new Client
                {
                    ClientId = "054",
                    ClientSecrets = new[] { new Secret("sujith_acharya".ToSha256()) },

                    // client-to-client communication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = new[] { "ApiOne" }
                },
                new Client
                {
                    ClientId = "054_mvc",
                    ClientSecrets = new[] { new Secret("sujith_acharya_mvc".ToSha256()) },

                    // user-to-client communication
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = new[] { "https://localhost:44399/signin-oidc" },

                    AllowedScopes = new[] {
                        "ApiOne",
                        "ApiTwo",
                        OidcConstants.StandardScopes.OpenId,
                        OidcConstants.StandardScopes.Profile,
                    },

                    RequireConsent = false,
                }
            };
    }
}
