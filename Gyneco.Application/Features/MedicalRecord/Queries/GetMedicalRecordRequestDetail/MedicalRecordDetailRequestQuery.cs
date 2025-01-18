using MediatR;

namespace Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestDetail;

public class MedicalRecordDetailRequestQuery : IRequest<MedicalRecordDetailDTO>
{
    public Guid Id { get; set; }
}