using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IdentityServerService.Shared.Models
{
    public class RegisterResponse
    {
        public bool Successful { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
