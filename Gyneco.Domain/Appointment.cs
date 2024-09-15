using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Domain
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ReasonForVisit { get; set; }
        public string Status { get; set; } // Scheduled, Completed, Canceled, etc.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }

}
