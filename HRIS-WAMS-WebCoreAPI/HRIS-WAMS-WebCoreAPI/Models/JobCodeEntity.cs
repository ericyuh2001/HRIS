using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace HRIS_WAMS_WebCoreAPI.Models
{


    public class WorkingDateJobCodebyEmpIDEndity
    {

        [Required]
        [Display(Name = "Type代碼")]
        public string TypeCode { get; set; }

        [Required]
        [Display(Name = "Type名稱")]
        public string TypeName { get; set; }

        [Required]
        [Display(Name = "Job代碼")]
        public string JobCode { get; set; }

        [Required]
        [Display(Name = "Job名稱")]
        public string JobName { get; set; }
        
    }
}
