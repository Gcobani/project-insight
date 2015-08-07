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
        public int QualificationCode { get; set; }
        public string QualificationName { get; set; }
        public string Id { get; set; }
    }
}
