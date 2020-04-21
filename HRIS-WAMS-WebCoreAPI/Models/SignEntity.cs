using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class SignEntity
    {
    }

    public class WaitApproveEntity
    {
        [Required]
        [Display(Name = "姓名")]
        public string EmpName { get; set; }


        [Required]
        [Display(Name = "簽核待批訊息")]
        public string message { get; set; }

    }


}
