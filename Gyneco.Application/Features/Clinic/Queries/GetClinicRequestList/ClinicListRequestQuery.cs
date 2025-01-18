using Gyneco.Application.Features.Clinic.Queries.GerClinicRequestDetail;
using Gyneco.Application.Models.Search;
using MediatR;

namespace Gyneco.Application.Features.Clinic.Queries.GetClinicRequestList;

public class ClinicListRequestQuery : IRequest<SearchResult<ClinicDetailDTO>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public Dictionary<string, string> Filters { get; set; }
}