using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyneco.Domain.Common;

namespace Gyneco.Domain
{
    public class Schedule : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Doctor Doctor { get; set; }
    }

}
