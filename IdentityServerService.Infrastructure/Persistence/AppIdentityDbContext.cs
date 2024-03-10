using System;
using System.Collections.Generic;
using System.Text;
using IdentityServerService.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerService.Infrastructure.Persistence
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = AccountTypeEnum.Admin.ToString(),
                NormalizedName = AccountTypeEnum.Admin.ToString().ToUpper(),
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),

            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = AccountTypeEnum.AdvisorAccount.ToString(),
                NormalizedName = AccountTypeEnum.AdvisorAccount.ToString().ToUpper(),
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),

            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = AccountTypeEnum.EmployerAccount.ToString(),
                NormalizedName = AccountTypeEnum.EmployerAccount.ToString().ToUpper(),
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),

            });
        }
    }
}
