using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class EmpLeaveWorkDateEntity
    {
        [Required]
        [Display(Name ="員工代碼")]
        public string EmpID { get; set; }

        [Required]
        [Display(Name ="上班日")]
        public DateTime WorkDate { get; set; }

        [Display(Name ="套用時數")]
        public int ApplyHours { get; set; }

        public string Reason { get; set; }
        public string type { get; set; }

        
        public string IsProject { get; set; }

    }
}
