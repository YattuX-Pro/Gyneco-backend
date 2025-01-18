using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.Doctor.Commands.CreateDoctor;

public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    private readonly IUnitOfWork _uow;
    public CreateDoctorCommandValidator( IUnitOfWork uow)
    {
        _uow = uow;
        
        RuleFor(p => p.FirstName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(p => p.LastName).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(p => p.Email).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(p => p.PhoneNumber).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(p => p.Specialty).NotNull().IsInEnum();
        RuleFor(p => p.UserId).NotEmpty().NotNull().MustAsync(UserMustExistAsync);
    }
    
    private async Task<bool> UserMustExistAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _uow.UserService.UserExistAsync(userId);
    }
}