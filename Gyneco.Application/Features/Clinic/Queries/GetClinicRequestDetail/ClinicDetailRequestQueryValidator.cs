using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Clinic.Queries.GerClinicRequestDetail;

public class ClinicDetailRequestQueryValidator : AbstractValidator<ClinicDetailRequestQuery>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClinicDetailRequestQueryValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(p=> p.Id).NotEmpty().NotNull().WithMessage("Id cannot be empty")
            .MustAsync(ClinicExistsAsync).WithMessage("Clinic should exist");
    }
    
    private async Task<bool> ClinicExistsAsync(Guid clinicId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ClinicRepository.ExistsAsync(x => x.Id == clinicId);
    }
}