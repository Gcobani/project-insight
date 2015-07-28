using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight
{
    public class StudentAttendance
    {
        public DateTime DateTime { get; set; }
        public string VenueCode { get; set; }
        public string ModuleCode { get; set; }
        //student number neeeded here
        public bool Present { get; set; }
    }
}
