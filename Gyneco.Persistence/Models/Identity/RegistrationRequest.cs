
using System.ComponentModel.DataAnnotations;

namespace Gyneco.Persistence.Models.Identity
{
    public class RegistrationRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        public string[] Roles { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
