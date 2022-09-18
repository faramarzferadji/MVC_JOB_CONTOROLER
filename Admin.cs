using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tornadu.Models
{
    public class Admin
    {
        [Range(100, 999)]
        [Display(Name = "PassWord")]
        public int Password { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}
