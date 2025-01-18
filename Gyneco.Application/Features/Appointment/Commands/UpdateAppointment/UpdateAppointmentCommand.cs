using MediatR;

namespace Gyneco.Application.Features.Appointment.Commands.UpdateAppointment;

public class UpdateAppointmentCommand : Domain.Appointment, IRequest<Unit>
{
    
}