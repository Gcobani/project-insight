using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class AttendanceRegister
    {
        public int AttendanceRegNumber { get; set; }
        public string VenueCode { get; set; }
        public DateTime DateTime { get; set; }
        public string ModuleCode { get; set; }
        public string StaffNumber { get; set; }
    }
}
