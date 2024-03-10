using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
                    Name = "openid",
                    DisplayName = "Open ID",
                    Required = true,
                    UserClaims = new List<string> { "sub" }
                },
                new IdentityResource
                {
                    Name = "profile",
                    DisplayName = "User Profile",
                    Emphasize = true,
                    UserClaims = new List<string> { "name", "family_name", "given_name", "middle_name", "preferred_username", "profile", "picture", "website", "gender", "birthdate", "zoneinfo", "locale", "updated_at", "role" }
                }
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("API.read"), new ApiScope("API.write"), };
        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("API")
                {
                    Name = "portal-resource",
                    DisplayName = "Portal API Resource",
                    Scopes = new List<string> { "API.read", "API.write" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "id", "name", "email", "role" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "portal.client",
                    ClientName = "Portal Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedScopes = { "API.read", "API.write", "offline_access" },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RequireConsent= false,
                    RequireClientSecret = true
                }
            };
    }
}
