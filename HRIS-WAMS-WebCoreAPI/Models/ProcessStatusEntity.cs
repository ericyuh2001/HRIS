using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace HRIS_WAMS_WebCoreAPI.Models
{
    //public class ProcessStatusEntity
    //{

    //    [Required]
    //    [Display(Name = "申請人")]
    //    public string ApplyID { get; set; }

    //    [Required]
    //    [Display(Name = "填報日期起始日")]
    //    public DateTime WorkingDateStart { get; set; }

    //    [Required]
    //    [Display(Name = "填報日期終止日")]
    //    public DateTime WorkingDateEnd { get; set; }

    //    [Required]
    //    [Display(Name = "建檔人員")]
    //    public string CreatedBy { get; set; }

    //    [Required]
    //    [Display(Name = "建檔時間")]
    //    public DateTime CreatedDate { get; set; }
    //}




    public class ProcessStatusEntityText
    {

        [Required]
        [Display(Name = "申請人")]
        public string ApplyID { get; set; }

        [Required]
        [Display(Name = "填報日期起始日")]
        public string WorkingDateStart { get; set; }

        [Required]
        [Display(Name = "填報日期終止日")]
        public string WorkingDateEnd { get; set; }

      
    }


}
