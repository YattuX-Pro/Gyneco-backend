using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestDetail;

public class AppointmentDetailRequestQueryHandler : IRequestHandler<AppointmentDetailRequestQuery, AppointmentDetailDTO>
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentDetailRequestQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<AppointmentDetailDTO> Handle(AppointmentDetailRequestQuery request, CancellationToken cancellationToken)
    {
        var validator = new AppointmentDetailRequestValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if(!validationResult.Errors.Any())
            throw new BadRequestException("Invalid request", validationResult);
        var appointment = await _unitOfWork.AppointmentRepository.FindAsync(request.Id);
        var appointmentDTO = appointment.ToDTO<Domain.Appointment, AppointmentDetailDTO>();
        return appointmentDTO;
    }
}