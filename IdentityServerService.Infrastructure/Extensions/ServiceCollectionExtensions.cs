using IdentityServer4.Services;
using IdentityServerService.Infrastructure.Persistence;
using IdentityServerService.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServerService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityServerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+'#!/^%{}*";
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
              .AddDefaultTokenProviders();

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("IdentitySSConnection"));
                        options.EnableTokenCleanup = true;
                    })
                     .AddConfigurationStore(options =>
                     {
                         options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("IdentitySSConnection"));
                     })
                    .AddAspNetIdentity<AppUser>();
            return services;
        }
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<AppPersistedGrantDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<AppConfigurationDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
        public static IServiceCollection AddServices<TUser>(this IServiceCollection services) where TUser : IdentityUser<int>, new()
        {
            services.AddTransient<IProfileService, IdentityClaimsProfileService>();
            return services;
        }
    }
}
