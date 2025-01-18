using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Doctor.Commands.CreateDoctor;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    public CreateDoctorCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<Unit> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateDoctorCommandValidator(_uow);
        var validatorResults = await validator.ValidateAsync(request);
        if (validatorResults.Errors.Any())
            throw new BadRequestException("Invalid request", validatorResults);
        var doctor = request.ToNewEntity<CreateDoctorCommand, Domain.Doctor>();
        await _uow.DoctorRepository.CreateAsync(doctor);
        
        return Unit.Value;
    }
}