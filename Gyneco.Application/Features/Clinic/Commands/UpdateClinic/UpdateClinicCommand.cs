using MediatR;

namespace Gyneco.Application.Features.Clinic.Commands.UpdateClinic;

public class UpdateClinicCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}