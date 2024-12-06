using Gyneco.Application.Models.Search;
using MediatR;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestList;

public class GetPatientRequestQuery : IRequest<SearchResult<GetPatientRequestDTO>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public Dictionary<string, string> Filters { get; set; }
}