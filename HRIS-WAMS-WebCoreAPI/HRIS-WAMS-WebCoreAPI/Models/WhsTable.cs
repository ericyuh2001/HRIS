using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class WhsTable
    {
    }



    [Table("WorkingHours", Schema = "whs")]
    public class WorkingHoursEntity
    {
        [Key]
        [Required]
        [Display(Name = "工時填報資料代碼")]
        public string RowUnid { get; set; }

        [Required]
        [Display(Name = "員工代號")]
        public string EmpID { get; set; }


        [Required]
        [Display(Name = "上班日")]
        public DateTime WorkingDate { get; set; }


        [Display(Name = "當日已填報工時")]
        public decimal WorkingHours { get; set; }


        [Display(Name = "應填工時")]
        public decimal FilledHours { get; set; }


        [Display(Name = "是否已填報完成")]
        public string IsFinish { get; set; }

    }





}
