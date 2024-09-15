using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Domain
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Appointment Appointment { get; set; }
    }

}
