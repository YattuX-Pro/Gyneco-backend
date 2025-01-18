using Gyneco.Application.Models.Identity;

namespace Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestDetail;

public class DoctorDetailDTO : Domain.Doctor
{
    public UserModel User { get; set; }
}