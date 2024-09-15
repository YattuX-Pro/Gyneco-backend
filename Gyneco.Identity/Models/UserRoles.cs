using Gyneco.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Identity.Services
{
    public class UserRoles : IdentityUserRole<Guid>
    {
        public ApplicationUser User { get; set; }
        public ApplicationUserRoles Role { get; set; }
    }
}
