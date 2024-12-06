using Gyneco.Application.Features.Patient.Queries.GetPatientRequestList;
using MediatR;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestDetail;

public class GetPatientDetailRequestQuery : IRequest<GetPatientDetailDTO>
{
    public Guid Id { get; set; }
}