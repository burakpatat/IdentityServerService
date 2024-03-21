﻿using IdentityServer4.Models;
using IdentityServer4.Test;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using static IdentityServer4.IdentityServerConstants;
using GrantTypes = IdentityServer4.Models.GrantTypes;

namespace IdentityServerService.Infrastructure.Persistence
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("API.read"), new ApiScope("API.write") };
        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("portal-resource")
                {
                    Name = "portal-resource",
                    DisplayName = "Portal API Resource",
                    Scopes = new List<string> { "API.read", "API.write" },
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
                },
                new Client
                {
                    ClientId = "angular-client",
                    ClientName = "Angular Client Web",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "API.read", "API.write", StandardScopes.OpenId, StandardScopes.Profile },
                    RedirectUris =
                    {
                        "http://localhost:4200/callback"

                    },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" }
                }
            };
    }
}
