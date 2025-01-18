using MediatR;

namespace Gyneco.Application.Features.Clinic.Queries.GerClinicRequestDetail;

public class ClinicDetailRequestQuery : IRequest<ClinicDetailDTO>
{
    public Guid Id { get; set; }
}