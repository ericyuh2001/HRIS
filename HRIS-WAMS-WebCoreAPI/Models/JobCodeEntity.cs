using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace HRIS_WAMS_WebCoreAPI.Models
{


    public class JobCodeEntity
    {

        [Required]
        [Display(Name = "類型代碼")]
        public string TypeCode { get; set; }

        [Required]
        [Display(Name = "類型名稱")]
        public string TypeName { get; set; }

        [Required]
        [Display(Name = "工作代碼")]
        public string JobCode { get; set; }

        [Required]
        [Display(Name = "工作名稱")]
        public string JobName { get; set; }
        
    }
}
