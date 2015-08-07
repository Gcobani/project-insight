using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class Qualification
    {
        [Key]
        [Required]
        [Display(Name="Qualification Code")]
        public int QualificationCode { get; set; }
        [Required]
        [Display(Name="Qualification Name")]
        public string QualificationName { get; set; }
    }
}
