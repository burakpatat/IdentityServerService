using IdentityServerService.Application;
using IdentityServerService.Infrastructure.Persistence;
using IdentityServerService.Server.Models;
using IdentityServerService.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerService.Server.Controller
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            AppUser user = new AppUser()
            {
                NameSurname = model.NameSurname,
                UserName = model.Mail,
                Email = model.Mail,
                AccountType = (int)model.AccountType,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResponse { Successful = false, Errors = errors });
            }

            if (user.Email!.ToLower().StartsWith("admin"))
            {
                user.AccountType = 1;

                return Ok(new RegisterResponse { Successful = true });
            }

            await _userManager.AddClaimAsync(user, new Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new Claim("name", user.NameSurname));
            await _userManager.AddClaimAsync(user, new Claim("email", user.Email));
            await _userManager.AddClaimAsync(user, new Claim("role", Enum.GetName(typeof(AccountTypeEnum),user.AccountType) ));

            return Ok(new RegisterResponse { Successful = true });

        }
    }
}
