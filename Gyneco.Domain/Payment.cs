using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // Cash, Credit Card, Insurance, etc.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Appointment Appointment { get; set; }
    }

}
