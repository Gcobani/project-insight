using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Insight.Data;

namespace Insight.Models
{
    public class AttendanceViewModel
    {
        public IEnumerable<SelectListItem> Modules { get; set; }
        public IEnumerable<SelectListItem> Venues { get; set; }
        public IEnumerable<SelectListItem> Lectures { get; set; }
        public Lecture Lecture { get; set; }
    }
}