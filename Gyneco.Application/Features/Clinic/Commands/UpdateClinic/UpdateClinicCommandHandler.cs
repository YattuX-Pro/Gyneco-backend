using Gyneco.Application.Exceptions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Clinic.Commands.UpdateClinic;

public class UpdateClinicCommandHandler : IRequestHandler<UpdateClinicCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClinicCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(UpdateClinicCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateClinicCommandValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid request", validationResult);
        
        var clinic = await _unitOfWork.ClinicRepository.FindAsync(request.Id);
        clinic.Name = request.Name;
        clinic.Address = request.Address;
        clinic.PhoneNumber = request.PhoneNumber;
        clinic.Email = request.Email;
        
        await _unitOfWork.ClinicRepository.UpdateAsync(clinic);
        
        return Unit.Value;
    }
}