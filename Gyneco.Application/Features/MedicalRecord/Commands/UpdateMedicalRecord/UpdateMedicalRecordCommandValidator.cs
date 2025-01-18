using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.MedicalRecord.Commands.UpdateMedicalRecord;

public class UpdateMedicalRecordCommandValidator : AbstractValidator<UpdateMedicalRecordCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateMedicalRecordCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("Id is required.")
            .MustAsync(MedicalRecordExistsAsync).WithMessage("MedicalRecord should exist.");
        RuleFor(p => p.PatientId).NotEmpty().Null().WithMessage("Patient Id cannot be empty")
            .MustAsync(PatientExistsAsync);
        RuleFor(p=> p.Description).NotEmpty().NotNull().WithMessage("Description cannot be empty");
        RuleFor(p=> p.RecordType).NotEmpty().NotNull().WithMessage("Record Type cannot be empty");
        RuleFor(p=> p.DateOfRecord).NotEmpty().NotNull().WithMessage("Date of record cannot be empty");
    }
    
    private async Task<bool> PatientExistsAsync(Guid patientId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.PatientRepository.ExistsAsync(x => x.Id == patientId);
    }
    
    private async Task<bool> MedicalRecordExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.MedicalRecordRepository.ExistsAsync(x => x.Id == id);
    }
    
}