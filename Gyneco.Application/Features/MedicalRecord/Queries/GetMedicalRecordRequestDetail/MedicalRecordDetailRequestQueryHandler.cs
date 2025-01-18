using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestDetail;

public class MedicalRecordDetailRequestQueryHandler : IRequestHandler<MedicalRecordDetailRequestQuery, MedicalRecordDetailDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    public MedicalRecordDetailRequestQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<MedicalRecordDetailDTO> Handle(MedicalRecordDetailRequestQuery request, CancellationToken cancellationToken)
    {
        var validator = new MedicalRecordDetailRequestValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if(!validationResult.IsValid)
            throw new BadRequestException("Invalid request", validationResult);
        
        var medicalRecord = await _unitOfWork.MedicalRecordRepository.FindAsync(x => x.Id == request.Id);
        var medicalRecordDetailDTO = medicalRecord.ToDTO<Domain.MedicalRecord, MedicalRecordDetailDTO>();
        return medicalRecordDetailDTO;
    }
}