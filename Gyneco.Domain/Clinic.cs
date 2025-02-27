﻿using Gyneco.Domain.Common;

namespace Gyneco.Domain
{
    public class Clinic : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }

}
