using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestDetail;

public class MedicalRecordDetailRequestValidator : AbstractValidator<MedicalRecordDetailRequestQuery>
{
    private readonly IUnitOfWork _unitOfWork;

    public MedicalRecordDetailRequestValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(p=> p.Id).NotNull().NotNull().WithMessage("{PropertyName} is required.")
            .MustAsync(MedicalRecordExistsAsync).WithMessage("{PropertyName} is should exists.");
    }
    
    private async Task<bool> MedicalRecordExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.MedicalRecordRepository.ExistsAsync(x => x.Id == id);
    }
}