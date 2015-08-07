using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insight.Models
{
    public class CreateModuleViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        int key { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int NumberOfScheduledClasses { get; set; }
        public IEnumerable<SelectListItem> Qualifications { get; set; }
    }
}