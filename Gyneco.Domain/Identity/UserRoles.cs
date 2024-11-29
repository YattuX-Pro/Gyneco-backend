using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Domain.Identity
{
    public class UserRoles : IdentityUserRole<Guid>
    {
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUserRoles Role { get; set; }
    }
}
