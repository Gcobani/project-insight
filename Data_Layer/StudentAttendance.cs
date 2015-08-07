using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class StudentAttendance
    {
        public DateTime DateTime { get; set; }
        public string VenueCode { get; set; }
        public string ModuleCode { get; set; }
        public string StudentNumber { get; set; }
        public bool Present { get; set; }
    }
}
