using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.Clinic.Commands.UpdateClinic;

public class UpdateClinicCommandValidator : AbstractValidator<UpdateClinicCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateClinicCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(p => p.Email).NotEmpty().NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");
        RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Name is required");
        RuleFor(p => p.PhoneNumber).NotEmpty().NotNull().WithMessage("Phone number is required");
        RuleFor(p=> p.Id).NotEmpty().NotNull().WithMessage("Id is required")
            .MustAsync(ClinicExistsAsync).WithMessage("Clinic should exist");
    }

    private async Task<bool> ClinicExistsAsync(Guid clinicId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ClinicRepository.ExistsAsync(x => x.Id == clinicId);
    }
}