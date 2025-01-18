using MediatR;

namespace Gyneco.Application.Features.Clinic.Commands.CreateClinic;

public class CreateClinicCommand : Domain.Clinic, IRequest<Unit>
{
    
}