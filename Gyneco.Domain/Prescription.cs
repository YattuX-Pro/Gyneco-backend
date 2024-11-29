using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyneco.Domain.Common;

namespace Gyneco.Domain
{
    public class Prescription : BaseEntity
    {
        public Guid AppointmentId { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public Appointment Appointment { get; set; }
    }

}
