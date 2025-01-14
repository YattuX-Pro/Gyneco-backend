using Gyneco.Application.Models.Identity;
using Gyneco.Domain;
using Gyneco.Domain.Identity;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestDetail;

public class GetPatientDetailDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}