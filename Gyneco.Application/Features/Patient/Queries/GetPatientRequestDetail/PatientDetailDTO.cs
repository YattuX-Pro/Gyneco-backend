using Gyneco.Application.Models.Identity;
using Gyneco.Domain;
using Gyneco.Domain.Identity;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestDetail;

public class PatientDetailDTO : Domain.Patient
{
    public UserModel User { get; set; }
    public ICollection<Domain.Appointment> Appointments { get; set; }
}