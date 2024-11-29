using Microsoft.AspNetCore.Identity;

namespace Gyneco.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<UserRoles> UserRoles { get; set; }
}