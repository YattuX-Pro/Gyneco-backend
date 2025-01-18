using MediatR;

namespace Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestDetail;

public class DoctorDetailRequestQuery : IRequest<DoctorDetailDTO>
{
    public Guid Id { get; set; }
}