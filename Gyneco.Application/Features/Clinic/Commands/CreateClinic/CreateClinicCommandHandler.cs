using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Clinic.Commands.CreateClinic;

public class CreateClinicCommandHandler : IRequestHandler<CreateClinicCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateClinicCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(CreateClinicCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateClinicCommandValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid request", validationResult);
        var clinic = request.ToNewEntity<CreateClinicCommand, Domain.Clinic>();
        await _unitOfWork.ClinicRepository.CreateAsync(clinic);
        return Unit.Value;
    }
}