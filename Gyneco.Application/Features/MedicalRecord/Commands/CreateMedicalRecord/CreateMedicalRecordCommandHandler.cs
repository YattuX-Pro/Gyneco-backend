using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.MedicalRecord.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMedicalRecordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateMedicalRecordCommandValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if(!validationResult.IsValid)
            throw new BadRequestException("Invalid request", validationResult);

        var medicalRecord = request.ToNewEntity<CreateMedicalRecordCommand, Domain.MedicalRecord>();
        await _unitOfWork.MedicalRecordRepository.CreateAsync(medicalRecord);
        return Unit.Value;
    }
}