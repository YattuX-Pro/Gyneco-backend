using MediatR;

namespace Gyneco.Application.Features.MedicalRecord.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommand : IRequest<Unit>
{
    public Guid PatientId { get; set; }
    public string RecordType { get; set; } 
    public DateTime DateOfRecord { get; set; }
    public string Description { get; set; }
}