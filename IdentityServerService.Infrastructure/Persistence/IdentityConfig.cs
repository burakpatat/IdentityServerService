using IdentityServer4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static IdentityModel.OidcConstants;
using GrantTypes = IdentityServer4.Models.GrantTypes;

namespace IdentityServerService.Infrastructure.Persistence
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
           new[]
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("API.read"), new ApiScope("API.write"), new ApiScope("offline_access") };
        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("API")
                {
                    Name = "portal-resource",
                    DisplayName = "Portal API Resource",
                    Scopes = new List<string> { "API.read", "API.write", "offline_access" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "id", "name", "email", "role" },
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "portal.client",
                    ClientName = "Portal Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedScopes = { "API.read", "API.write", StandardScopes.OpenId },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RequireConsent= false,
                    RequireClientSecret = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    Enabled = true
                }
            };
    }
}
