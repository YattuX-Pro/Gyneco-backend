using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Clinic.Queries.GerClinicRequestDetail;

public class ClinicDetailRequestQueryHandler : IRequestHandler<ClinicDetailRequestQuery, ClinicDetailDTO>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClinicDetailRequestQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ClinicDetailDTO> Handle(ClinicDetailRequestQuery request, CancellationToken cancellationToken)
    {
        var validator = new ClinicDetailRequestQueryValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if(!validationResult.IsValid)
            throw new BadRequestException("Invalid request", validationResult);

        var clinic = await _unitOfWork.ClinicRepository.FindAsync(request.Id);

        var clinicDTO = clinic.ToDTO<Domain.Clinic, ClinicDetailDTO>();
        
        return clinicDTO;
    }
}