
namespace Gyneco.Application.Models.Identity
{
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identifiant { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Role { get; set; }
        public string Token { get; set; }
        public DateTime DateTokenExpiration { get; set; }
    }
}
