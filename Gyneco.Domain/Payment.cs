using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyneco.Domain.Common;

namespace Gyneco.Domain
{
    public class Payment : BaseEntity
    {
        public Guid AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } 
        public Appointment Appointment { get; set; }
    }

}
