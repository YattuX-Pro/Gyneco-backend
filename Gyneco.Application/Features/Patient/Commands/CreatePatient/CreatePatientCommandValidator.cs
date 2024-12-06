using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;
using Gyneco.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gyneco.Application.Features.Patient.Commands.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public CreatePatientCommandValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        
        RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(p => p.LastName).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Email).NotEmpty().MaximumLength(50);
        RuleFor(p => p.PhoneNumber).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Address).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Gender).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Email).NotEmpty().MaximumLength(50);
        RuleFor(p => p.UserId).NotNull().MustAsync(UserMustExist).WithMessage("{PropertyName} is not found");
    }

    private async Task<bool> UserMustExist(Guid userId, CancellationToken cancellationToken)
    {
        return _userManager.Users.Any(u => u.Id == userId);
    }
}