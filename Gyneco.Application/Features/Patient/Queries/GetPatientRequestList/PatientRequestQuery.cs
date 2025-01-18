using Gyneco.Application.Features.Patient.Queries.GetPatientRequestDetail;
using Gyneco.Application.Models.Search;
using MediatR;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestList;

public class PatientRequestQuery : IRequest<SearchResult<PatientDetailDTO>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public Dictionary<string, string> Filters { get; set; }
}