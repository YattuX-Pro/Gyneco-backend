using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Appointment.Commands.UpdateAppointment;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateAppointmentCommandValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.Errors.Any())
            throw new BadRequestException("Invalid request", validationResult);
        var appointment = request.ToNewEntity<UpdateAppointmentCommand, Domain.Appointment>();
        await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
        return Unit.Value;
    }
}