using Gyneco.Application.Exceptions;
using Gyneco.Application.Extensions;
using Gyneco.Domain.Contracts.UnitOfWork;
using Gyneco.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Gyneco.Application.Features.Patient.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Unit>
{
    private IMediator _mediator;
    private readonly IUnitOfWork _uow;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreatePatientCommandHandler( IMediator mediator, IUnitOfWork uow, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _uow = uow;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreatePatientCommandValidator(_userManager);
        var validationResults = await validator.ValidateAsync(request);
        
        if(validationResults.Errors.Any())
            throw new BadRequestException("Invalid request", validationResults);
        var patient = request.ToNewEntity<CreatePatientCommand, Domain.Patient>();
        await _uow.PatientRepository.CreateAsync(patient);
        return Unit.Value;
    }
}