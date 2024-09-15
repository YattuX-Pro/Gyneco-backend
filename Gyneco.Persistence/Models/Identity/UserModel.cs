

namespace Gyneco.Persistence.Models.Identity
{
    public class UserModel 
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public List<RoleInfo> Roles { get; set; }
        public List<RoleInfo> UserRoles { get; set; }
    }
}
