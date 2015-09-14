using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Insight.Data;
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
        public IEnumerable<SelectListItem> StaffMembers { get; set; }
    }

    public class Bridge
    {
        public IEnumerable<String> User_Id { get; set; }
        public int Lecture_Id { get; set; }
        public string[] dateSlice { get; set; }
    }

    public class CloserViewModel
    {
        public List<Student> Students { get; set; }
        public Lecture Lecture { get; set; }
        public string Date { get; set; }
        public Lecturer Lecturer { get; set; }
    }

    public class MonthlyStats
    {
        public IEnumerable<SelectListItem> Modules { get; set; }

        public string Month { get; set; }
    }
}