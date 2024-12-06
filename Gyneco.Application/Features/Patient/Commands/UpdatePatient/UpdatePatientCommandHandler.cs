using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Patient.Commands.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    public UpdatePatientCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdatePatientCommandValidator(_uow);
        var validatorResults = await validator.ValidateAsync(request, cancellationToken);
        
        if (validatorResults.Errors.Any())
            throw new BadRequestException("Invalid request", validatorResults);

        var patient = request.ToNewEntity<UpdatePatientCommand, Domain.Patient>();

        await _uow.PatientRepository.UpdateAsync(patient);
        return Unit.Value;
    }
}