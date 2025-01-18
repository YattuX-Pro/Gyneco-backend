using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.MedicalRecord.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommandValidator : AbstractValidator<CreateMedicalRecordCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateMedicalRecordCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(p => p.PatientId).NotEmpty().Null().WithMessage("Patient Id cannot be empty")
            .MustAsync(PatientExistsAsync).WithMessage("Patient Id Not exists");
        RuleFor(p=> p.Description).NotEmpty().NotNull().WithMessage("Description cannot be empty");
        RuleFor(p=> p.RecordType).NotEmpty().NotNull().WithMessage("Record Type cannot be empty");
        RuleFor(p=> p.DateOfRecord).NotEmpty().NotNull().WithMessage("Date of record cannot be empty");
    }

    private async Task<bool> PatientExistsAsync(Guid patientId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.PatientRepository.ExistsAsync(x => x.Id == patientId);
    }
}