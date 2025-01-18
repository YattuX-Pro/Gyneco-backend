using Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestDetail;
using Gyneco.Application.Models.Search;
using MediatR;

namespace Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestList;

public class AppointmentListRequestQuery : IRequest<SearchResult<AppointmentDetailDTO>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public Dictionary<string, string> Filters { get; set; }
}