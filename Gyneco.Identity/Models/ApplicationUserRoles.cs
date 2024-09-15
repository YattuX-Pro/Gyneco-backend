using Gyneco.Identity.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Identity.Models
{
    public class ApplicationUserRoles : IdentityRole<Guid>
    {
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
