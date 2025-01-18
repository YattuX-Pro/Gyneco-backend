using MediatR;

namespace Gyneco.Application.Features.MedicalRecord.Commands.UpdateMedicalRecord;

public class UpdateMedicalRecordCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string RecordType { get; set; } 
    public DateTime DateOfRecord { get; set; }
    public string Description { get; set; }
}