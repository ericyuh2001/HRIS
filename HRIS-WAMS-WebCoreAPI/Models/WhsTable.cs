using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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














    [Table("ProcessStatus", Schema = "whs")]
    public class ProcessStatusEntity
    {
        [Key]
        public string FlowID { get; set; }

        public DateTime ApplyDate { get; set; }

        public string DeptMergeCode { get; set; }

        [AllowNull]
        public DateTime WorkingDateStart { get; set; }

        [AllowNull]
        public DateTime WorkingDateEnd { get; set; }
        public int WorkingDateItemCount { get; set; }
        public string  IsFinish { get; set; }
        public string IsDeleted { get; set; }

        [AllowNull]
        public string SignID { get; set; }

        [AllowNull]
        public DateTime SignDate { get; set; }

        [AllowNull]
        public string SingRemark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [AllowNull]
        public string UpdatedBy { get; set; }

        [AllowNull]
        public DateTime UpdatedDate { get; set; }

        [AllowNull]
        public string DeletedBy { get; set; }

        [AllowNull]
        public DateTime DeletedDate { get; set; }
    }




    [Table("ProcessStatusDetail", Schema = "whs")]
    public class ProcessStatusDetailEntity
    {
        [Key]
        public string FlowID { get; set; }

        [Key]
        public string RowUnid { get; set; }

        public int SerialNo { get; set; }

    }











}
