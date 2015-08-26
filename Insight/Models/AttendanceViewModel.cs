using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insight.Models
{
    public class AttendanceViewModel
    {
        public IEnumerable<SelectListItem> Modules { get; set; }
    }
}