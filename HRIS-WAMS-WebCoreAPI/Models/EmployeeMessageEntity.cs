using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class EmployeeMessageEntity
    {
        [Required]
        [Display(Name = "提醒訊息")]
        public string Message { get; set; }
    }
}
