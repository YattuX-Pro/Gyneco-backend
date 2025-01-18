using Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestDetail;
using Gyneco.Application.Models.Search;
using MediatR;

namespace Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestList;

public class DoctorListRequestQuery : IRequest<SearchResult<DoctorDetailDTO>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public Dictionary<string, string> Filters { get; set; }
}