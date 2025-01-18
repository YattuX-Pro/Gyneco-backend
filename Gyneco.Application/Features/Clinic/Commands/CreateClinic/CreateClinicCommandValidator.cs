using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.Clinic.Commands.CreateClinic;

public class CreateClinicCommandValidator : AbstractValidator<CreateClinicCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateClinicCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(p => p.Address).NotEmpty().NotNull().WithMessage("Address is required");
        RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Name is required");
        RuleFor(p => p.PhoneNumber).NotEmpty().NotNull().WithMessage("Phone number is required");
        RuleFor(p => p.Email).NotEmpty().NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");
    }
}