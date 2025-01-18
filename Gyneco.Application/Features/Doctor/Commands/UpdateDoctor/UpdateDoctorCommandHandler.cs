using FluentValidation;
using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Doctor.Commands.UpdateDoctor;

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    public UpdateDoctorCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<Unit> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateDoctorCommandValidator(_uow);
        var validatorResults = await validator.ValidateAsync(request);
        if (validatorResults.Errors.Any())
            throw new BadRequestException("Invalid request", validatorResults);
        var doctor = request.ToNewEntity<UpdateDoctorCommand, Domain.Doctor>();
        await _uow.DoctorRepository.CreateAsync(doctor);
        return Unit.Value;
    }
}