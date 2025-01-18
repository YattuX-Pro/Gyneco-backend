using Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestDetail;
using Gyneco.Application.Models.Search;
using MediatR;

namespace Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestList;

public class MedicalRecordListRequestQuery : IRequest<SearchResult<MedicalRecordDetailDTO>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public Dictionary<string, string> Filters { get; set; }
}