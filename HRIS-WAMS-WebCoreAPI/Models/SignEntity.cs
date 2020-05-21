using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Security.Policy;

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
        [Display(Name = "待批表單代碼")]
        public string FlowID { get; set; }

        [Required]
        [Display(Name = "待批表單描述")]
        public string message { get; set; }

    }





    // 批核工時-待批月曆及單日批核總計狀態
    public class WaitApproveDayStaticEntity
    {
        [Key]
        [Display(Name = "簽核表單編號")]
        public string FlowID { get; set; }


        [Display(Name = "簽核狀態")]
        public string IsFinish { get; set; }

        [Key]
        [JsonIgnore]
        [Display(Name = "工作日")]
        public DateTime WorkingDate { get; set; }


        [Display(Name = "工作日")]
        [JsonPropertyName("workDate")]
        public string WorkDateString
        {
            get
            {
                return WorkingDate.ToString("yyyy-MM-dd");
            }
            set
            {

            }
        }

        [Display(Name = "待批案件筆數")]
        public int WaitApproveItemCount { get; set; }
    }



    // 主管週間工時待批列表：合併
    public class WaitApproveStaticAndDetailEntity
    {
        [Display(Name = "週間工時申請待批統計")]
        public WaitApproveStaticEntity WaitApproveStatic { get; set; }

        [Display(Name = "週間工時申請待批明細")]
        public List<WaitApproveDetailEntity> WaitApproveList { get; set; }
    }


    // 主管週間工時待批列表：統計
    public class WaitApproveStaticEntity
    {
        int m_ItemCount = 0;

        public WaitApproveStaticEntity(int Value)
        {
            m_ItemCount = Value;
        }

        [Display(Name = "待批筆數")]
        public int ItemCount 
        { 
            get
            {
                return m_ItemCount;
            }
            set
            {
                m_ItemCount = value;
            }
        }
    }


    // 主管週間工時待批列表：明細
    public class WaitApproveDetailEntity
    {
        [Display(Name = "簽核表單編號")]
        public string FlowID { get; set; }


        [Display(Name = "員工代碼")]
        public string EmpID { get; set; }


        [Display(Name = "員工姓名")]
        public string EmpName { get; set; }


        [Display(Name = "申請時數")]
        public decimal totalHour { get; set; }
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
