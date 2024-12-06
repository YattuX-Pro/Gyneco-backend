using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using Gyneco.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestDetail;

public class GetPatientDetailRequestQueryHandler : IRequestHandler<GetPatientDetailRequestQuery, GetPatientDetailDTO>
{
    private readonly IUnitOfWork _uow;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetPatientDetailRequestQueryHandler(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
    {
        _uow = uow;
        _userManager = userManager;
    }
    
    public async Task<GetPatientDetailDTO> Handle(GetPatientDetailRequestQuery request, CancellationToken cancellationToken)
    {
        var isPatientExit = await _uow.PatientRepository.ExistsAsync(x => x.Id == request.Id);
        if(!isPatientExit) return null;
        var patient = await _uow.PatientRepository.FindAsync(x => x.Id == request.Id);
        var patientDto = patient?.ToDTO<Domain.Patient, GetPatientDetailDTO>();
        patientDto.User = await _uow.UserService.GetUserAsync(patient.UserId);
        return patientDto;
    }
}