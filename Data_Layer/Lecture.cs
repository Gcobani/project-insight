using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class Lecture
    {
        public string ModuleCode { get; set; }
        public DateTime TimeOfDay { get; set; }
        public string VenueCode { get; set; }
        public string StaffNumber { get; set; }
    }
}
