using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestDetail;

public class DoctorDetailRequestQueryHandler : IRequestHandler<DoctorDetailRequestQuery, DoctorDetailDTO>
{
    private readonly IUnitOfWork _uow;

    public DoctorDetailRequestQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<DoctorDetailDTO> Handle(DoctorDetailRequestQuery request, CancellationToken cancellationToken)
    {
        var validator = new DoctorDetailRequestValidator(_uow);
        var validationResults = await validator.ValidateAsync(request);
        if (!validationResults.Errors.Any())
            throw new NotFoundException("Invalid request", validationResults);
        var doctor = await _uow.DoctorRepository.FindAsync(request.Id);
        
        var doctorDTO = doctor.ToDTO<Domain.Doctor, DoctorDetailDTO>();
        doctorDTO.User = await _uow.UserService.GetUserAsync(doctor.UserId);
        return doctorDTO;
    }
}