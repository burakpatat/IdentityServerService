using IdentityServerService.Application;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerService.Server.Models
{
    public class RegisterRequest
    {
        [Display(Name = "Name Surname")]
        [Required(ErrorMessage = "")]
        public string NameSurname { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "")]
        public string Mail { get; set; }

        [Display(Name = "Account Type")]
        [Required(ErrorMessage = "")]
        public AccountTypeEnum AccountType { get; set; }
    }
}
