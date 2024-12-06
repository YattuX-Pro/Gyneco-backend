using MediatR;

namespace Gyneco.Application.Features.Patient.Commands.CreatePatient;

public class CreatePatientCommand : IRequest<Unit>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public Guid UserId { get; set; }
}