namespace Gyneco.Application.DTOs.Search;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string NormalizedUsername { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string NormalizedEmail { get; set; }
    public List<string> userRoles { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
}