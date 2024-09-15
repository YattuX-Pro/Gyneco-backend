
using Gyneco.Domain;
using Gyneco.Identity.Services;
using Microsoft.AspNetCore.Identity;

namespace Gyneco.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
