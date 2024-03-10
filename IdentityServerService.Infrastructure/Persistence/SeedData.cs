using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityServerService.Infrastructure.Persistence
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider provider)
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            provider.GetRequiredService<AppIdentityDbContext>().Database.Migrate();
            provider.GetRequiredService<AppPersistedGrantDbContext>().Database.Migrate();
            provider.GetRequiredService<AppConfigurationDbContext>().Database.Migrate();

            var context = provider.GetRequiredService<AppConfigurationDbContext>();
            if (!context.Clients.Any())
            {
                foreach (var client in IdentityConfig.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityConfig.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in IdentityConfig.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in IdentityConfig.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
