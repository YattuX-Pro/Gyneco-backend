using MediatR;

namespace Gyneco.Application.Features.Appointment.Commands.CreateAppoitment;

public class CreateAppointmentCommand : Domain.Appointment, IRequest<Unit>
{
    
}