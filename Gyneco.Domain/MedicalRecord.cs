using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyneco.Domain.Common;

namespace Gyneco.Domain
{
    public class MedicalRecord : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string RecordType { get; set; } 
        public DateTime DateOfRecord { get; set; }
        public string Description { get; set; }
        public Patient Patient { get; set; }
    }

}
