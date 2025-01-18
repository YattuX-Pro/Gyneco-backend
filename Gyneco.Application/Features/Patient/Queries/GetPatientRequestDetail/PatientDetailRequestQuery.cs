using Gyneco.Application.Features.Patient.Queries.GetPatientRequestList;
using MediatR;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestDetail;

public class PatientDetailRequestQuery : IRequest<PatientDetailDTO>
{
    public Guid Id { get; set; }
}