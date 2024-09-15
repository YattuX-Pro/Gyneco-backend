using Gyneco.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Domain
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DoctorSpeciality Specialty { get; set; }  // Gynécologue, obstétricien, etc.
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IApplicationUser ApplicationUser { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }

}
