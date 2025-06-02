using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class AppUser:IdentityUser
    {
        public string? Image { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
