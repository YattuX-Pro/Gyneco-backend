using Gyneco.Application.Exceptions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.MedicalRecord.Commands.UpdateMedicalRecord;

public class UpdateMedicalRecordCommandHandler : IRequestHandler<UpdateMedicalRecordCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateMedicalRecordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(UpdateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateMedicalRecordCommandValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if(!validationResult.IsValid)
            throw new BadRequestException("Invalid request", validationResult);
        
        var medicalRecord = await _unitOfWork.MedicalRecordRepository.FindAsync(x => x.Id == request.Id);
        medicalRecord.RecordType = request.RecordType;
        medicalRecord.DateOfRecord = request.DateOfRecord;
        medicalRecord.Description = request.Description;
        medicalRecord.PatientId = request.PatientId;
        
        await _unitOfWork.MedicalRecordRepository.UpdateAsync(medicalRecord);
        return Unit.Value;
    }
}