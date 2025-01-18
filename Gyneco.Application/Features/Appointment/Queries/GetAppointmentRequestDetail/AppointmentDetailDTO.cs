namespace Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestDetail;

public class AppointmentDetailDTO
{
    public Guid DoctorId { get; set; }
    public Domain.Doctor Doctor { get; set; }
    public Guid PatientId { get; set; }
    public Domain.Patient Patient { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string ReasonForVisit { get; set; }
    public string Status { get; set; } 
}