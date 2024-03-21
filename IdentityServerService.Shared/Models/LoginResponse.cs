using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServerService.Shared.Models
{
    public class LoginResponse
    {
        public bool Successful { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }
    }
}
