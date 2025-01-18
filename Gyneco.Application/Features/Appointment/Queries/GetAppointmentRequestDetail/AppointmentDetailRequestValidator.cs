using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestDetail;

public class AppointmentDetailRequestValidator : AbstractValidator<AppointmentDetailRequestQuery>
{
    private readonly IUnitOfWork _unitOfWork;
    public AppointmentDetailRequestValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(p => p.Id).NotEmpty().WithMessage("Id cannot be empty")
            .NotNull().WithMessage("Id cannot be null")
            .MustAsync(AppointmentExistAsync).WithMessage("Appointment do not exists");
    }

    private async Task<bool> AppointmentExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AppointmentRepository.ExistsAsync(x => x.Id == id);
    }
}