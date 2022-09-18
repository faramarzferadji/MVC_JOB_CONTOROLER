using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tornadu.Models
{
    public class Job
    {
        [Range(1000, 10000)]
        [Display(Name = "Job Id")]
        public int JobNum { get; set; }
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Display(Name = "Job Search")]
        public string Jsearch { get; set; }
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Date Of Apply")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DOA { get; set; }
       
        [Display(Name = "Status")]
        public string Status { get; set; }
        
    }
}