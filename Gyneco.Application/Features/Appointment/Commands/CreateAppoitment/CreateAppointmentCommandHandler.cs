using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Appointment.Commands.CreateAppoitment;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateAppointmentCommandValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.Errors.Any())
            throw new BadRequestException("Invalid Request", validationResult);
        var appointment = request.ToNewEntity<CreateAppointmentCommand, Domain.Appointment>();
        await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
        return Unit.Value;
    }
}