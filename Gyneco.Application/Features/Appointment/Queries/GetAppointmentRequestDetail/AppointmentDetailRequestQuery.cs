using MediatR;

namespace Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestDetail;

public class AppointmentDetailRequestQuery : IRequest<AppointmentDetailDTO>
{
    public Guid Id { get; set; }
}