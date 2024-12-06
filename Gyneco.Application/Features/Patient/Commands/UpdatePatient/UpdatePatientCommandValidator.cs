using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.Patient.Commands.UpdatePatient;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    private readonly IUnitOfWork _uow;
    public UpdatePatientCommandValidator(IUnitOfWork uow)
    {
        _uow = uow;
        
        RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(p => p.LastName).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Email).NotEmpty().MaximumLength(50);
        RuleFor(p => p.PhoneNumber).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Address).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Gender).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Email).NotEmpty().MaximumLength(50);
        RuleFor(p => p.UserId).NotNull().MustAsync(UserMustExistAsync).WithMessage("{PropertyName} is not found");
        RuleFor(p => p.Id).NotNull().MustAsync(PatientMustExistAsync).WithMessage("{PropertyName} is not found");
    }   
    
    private async Task<bool> UserMustExistAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _uow.UserService.UserExistAsync(userId);
    }

    private async Task<bool> PatientMustExistAsync(Guid patientId, CancellationToken cancellationToken)
    {
        return await _uow.PatientRepository.ExistsAsync(x => x.Id == patientId);
    }
}