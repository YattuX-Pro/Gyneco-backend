using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Domain
{
    public class MedicalRecord
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string RecordType { get; set; } 
        public DateTime DateOfRecord { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Patient Patient { get; set; }
    }

}
