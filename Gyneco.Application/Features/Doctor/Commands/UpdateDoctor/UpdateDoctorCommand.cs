using Gyneco.Domain.Enums;
using MediatR;

namespace Gyneco.Application.Features.Doctor.Commands.UpdateDoctor;

public class UpdateDoctorCommand : IRequest<Unit>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DoctorSpeciality Specialty { get; set; } 
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid UserId { get; set; }
}