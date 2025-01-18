using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.Appointment.Commands.UpdateAppointment;

public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(p => p.DoctorId).NotEmpty().NotNull()
            .MustAsync(DoctorMustExistAsync).WithMessage("Doctor Id must exist.");
        RuleFor(p => p.PatientId).NotEmpty().NotNull()
            .MustAsync(PatientMustExistAsync).WithMessage("Patient Id must exist.");
        RuleFor(p => p.AppointmentDate).NotEmpty().NotNull()
            .WithMessage("Appointment Date cannot be empty.");
        RuleFor(p => p.Id).NotEmpty().NotNull().MustAsync(AppointmentMustExistAsync).WithMessage("Appointment Id must exist.");
        
    }
    
    private async Task<bool> DoctorMustExistAsync(Guid DoctorId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.DoctorRepository.ExistsAsync(x => x.Id == DoctorId);
    }
    
    private async Task<bool> AppointmentMustExistAsync(Guid AppointmentId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AppointmentRepository.ExistsAsync(x => x.Id == AppointmentId);
    }
    
    private async Task<bool> PatientMustExistAsync(Guid PatientId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.PatientRepository.ExistsAsync(x => x.Id == PatientId);
    }
}