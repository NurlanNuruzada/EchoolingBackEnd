using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecooling.Domain.Entites
{
    public class Info:BaseEntity
    {
        public int TotalStudentCount { get; set; }
        public int TotalCoursesCount { get; set; }
        public int TotalTeachersCount { get; set; }
        public int TotalEventsCount { get; set; }
    }
}
