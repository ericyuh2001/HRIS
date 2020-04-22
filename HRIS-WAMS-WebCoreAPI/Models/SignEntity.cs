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




    public class SingleSignActionBody
    {
        [Required]
        [Display(Name = "簽核表單編號")]
        public string FlowID { get; set; }


        [Display(Name = "簽核主管員工編號")]
        public string SignID { get; set; }

        [Display(Name = "是否核准")]
        public string IsApproval { get; set; }



        [Display(Name = "簽核備註")]
        public string Note { get; set; }
       

        [Display(Name = "單日簽核明細")]
        public SignLogDetailEntity SignLogDetailInfo { get; set; }
    }



    public class SignLogDetailEntity
    {
        [Display(Name = "單日工時填報資料代碼")]
        public  string RowUnid { get; set; }


        [Display(Name = "單日退件")]
        public string IsRejected { get; set; }


        //[Display(Name = "單日簽核備註")]
        //public string Note { get; set; }
    }






    public class BatchSignApprovalEntity
    {
      
        [Display(Name = "簽核主管員工編號")]
        public string SignID { get; set; }

        [Display(Name = "是否核准")]
        public string IsApproval { get; set; }


        [Display(Name = "核准筆數")]
        public int ApprovedItemCount { get; set; }


        [Display(Name = "批次核准明細")]
        public SignBatchDetailEntity SignBatchDetailInfo { get; set; }
    }




    public class SignBatchDetailEntity
    {
        [Required]
        [Display(Name = "簽核表單編號")]
        public string FlowID { get; set; }

        [Required]
        [Display(Name = "已填報工時")]
        public string WorkingHours { get; set; }

    }





}
