using FluentValidation;
using Gyneco.Domain.Contracts.UnitOfWork;

namespace Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestDetail;

public class DoctorDetailRequestValidator : AbstractValidator<DoctorDetailRequestQuery>
{
    private readonly IUnitOfWork _uow;

    public DoctorDetailRequestValidator(IUnitOfWork uow)
    {
        _uow = uow;
        
        RuleFor(p => p.Id).NotNull().NotEmpty().MustAsync(DoctorExistAsync);
    }

    private async Task<bool> DoctorExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _uow.DoctorRepository.ExistsAsync(x => x.Id == id);
    }
}