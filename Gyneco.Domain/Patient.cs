using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyneco.Domain.Common;
using Gyneco.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gyneco.Domain
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

}
