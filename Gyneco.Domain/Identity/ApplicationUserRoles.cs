using Microsoft.AspNetCore.Identity;

namespace  Gyneco.Domain.Identity
{
    public class ApplicationUserRoles : IdentityRole<Guid>
    {
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
