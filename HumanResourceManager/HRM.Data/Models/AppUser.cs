using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}
