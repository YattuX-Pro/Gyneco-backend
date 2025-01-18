using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.Doctor.Commands.UpdateDoctor;

public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
{
    private readonly IUnitOfWork _uow;
    public UpdateDoctorCommandValidator(IUnitOfWork uow)
    {
        _uow = _uow;

        RuleFor(p => p.FirstName).NotEmpty().NotEmpty().MaximumLength(50);
        RuleFor(p => p.LastName).NotEmpty().NotEmpty().MaximumLength(50);
        RuleFor(p => p.Email).NotEmpty().NotEmpty().MaximumLength(50);
        RuleFor(p => p.PhoneNumber).NotEmpty().NotEmpty().MaximumLength(20);
        RuleFor(p => p.Specialty).IsInEnum().NotEmpty().NotNull();
        RuleFor(p => p.UserId).MustAsync(UserMustExistAsync);
    }
    
    private async Task<bool> UserMustExistAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _uow.UserService.UserExistAsync(userId);
    }
}