using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class EmpDutyEntity
    {



    }







    #region "員工首頁資訊 & 首頁待填報列表"
    // 員工首頁資訊 & 首頁待填報列表
    public class HomeInfoWithAlterByEmpEntity
    {
        public HomeInfoByEmpEntity homeInfo { get; set; }
        public List<AlterbyEmpIDEntity> alterListInfo { get; set; }
        public List<WaitApproveEntity> waitApproveListInfo { get; set; }
    }


    // 員工首頁資訊
    public class HomeInfoByEmpEntity
    {
        [Required]
        [Display(Name = "員工代號")]
        public string EmpID { get; set; }


        [Required]
        [Display(Name = "員工姓名")]
        public string EmpName { get; set; }


        [Display(Name = "性別")]
        public string Sex { get; set; }


        [Display(Name = "提醒訊息")]
        public string message { get; set; }


        [Display(Name = "申請待辦")]
        public string WriteRedPoint { get; set; }

        [Display(Name = "待批待辦")]
        public string SignRedPoint { get; set; }

        [Display(Name = "角色代碼")]
        public string RoleID { get; set; }
    }




    // 首頁待填報列表
    public class AlterbyEmpIDEntity
    {
        [Required]
        [Display(Name = "提醒訊息")]
        public string EmpID { get; set; }

        [Required]
        [Display(Name = "工作日")]
        public string WorkingDate { get; set; }


        [Display(Name = "狀態訊息")]
        public string status { get; set; }
    }
    #endregion






    #region "GetEmpLeavebyWorkDate 員工首頁資訊"

    public class EmpLeaveWithWorkDateDetailEntity
    {
        // 員工單日填報統計資訊
        public EmpWorkdateEntity WorkDate { get; set; }

        // 員工週間每日狀態顯示
        public List<EmpWorkingDateStatusList> WeekList { get; set; }

        // 員工單日假單、加班單&判斷員工單日工時
        public List<EmpLeavebyWorkDateEntity> EmpLeaveList { get; set; }

        // 抓取員工工時填報明細
        public List<WorkingDateDetailListEntity> DetailList { get; set; }
    }


    // 員工週間每日狀態顯示
    public class EmpWorkingDateStatusList
    {
        [Required]
        [Display(Name = "工作日")]
        [JsonIgnore]
        public DateTime WorkingDate { get; set; }

        [Display(Name = "工作日")]
        [JsonPropertyName("workingDate")]
        public string WorkingDateString
        {
            get
            {
                return WorkingDate.ToString("yyyy-MM-dd");
            }
            set
            {

            }
        }

        [Display(Name = "申請案件狀態")]
        public string IsFinish  { get; set; }
    }


    // 員工單日填報統計資訊
    public class EmpWorkdateEntity
    {
        [Required]
        [Display(Name = "工時填報資料代碼")]
        public string RowUnid { get; set; }

        [Display(Name = "員工代碼")]
        public string EmpID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Display(Name = "上班日")]
        public DateTime WorkingDate { get; set; }

        [Display(Name = "上班日")]
        [JsonPropertyName("workingDate")]
        public string WorkingDateString 
        { 
            get 
            {
                return WorkingDate.ToString("yyyy-MM-dd");
            }
            set
            {

            } 
        }

        [Display(Name = "已填工時")]
        public decimal TotalWorkingHours { get; set; }

        [Display(Name = "應填工時")]
        public decimal FilledHours { get; set; }

        [Display(Name = "狀態")]
        public string IsFinish { get; set; }

    }



    // 員工單日假單、加班單&判斷員工單日工時
    public class EmpLeavebyWorkDateEntity
    {
        [Required]
        [Display(Name = "員工代碼")]
        public string EmpID { get; set; }

        [Required]
        [Display(Name = "上班日")]
        [JsonIgnore]
        public DateTime WorkDate { get; set; }

        [Display(Name = "上班日")]
        [JsonPropertyName("workDate")]
        public string WorkDateString
        {
            get
            {
                return WorkDate.ToString("yyyy-MM-dd");
            }
            set
            {

            }
        }

        [Display(Name = "套用時數")]
        public int ApplyHours { get; set; }

        [Display(Name = "備註及說明")]
        public string Reason { get; set; }

        [Display(Name = "工時類型")]
        public string type { get; set; }

        [Display(Name = "專案註記")]
        public char IsProject { get; set; }

    }





    // 員工單日工時紀錄列表
    public class WorkingDateDetailListEntity
    {
        [Required]
        [Display(Name = "工時填報資料代碼")]
        public string RowUnid { get; set; }


        [Display(Name = "工時類別代碼")]
        public string TypeCode { get; set; }


        [Display(Name = "工時類別名稱")]
        public string TypeName { get; set; }


        [Display(Name = "Job代碼")]
        public string JobCode { get; set; }


        [Display(Name = "Job名稱")]
        public string JobName { get; set; }



        [Display(Name = "已填工時")]
        public decimal WorkingHours { get; set; }


        [Display(Name = "備註")]
        public string Note { get; set; }
    }

    //public class WorkingDateAllDetailEntity
    //{
    //    [Required]
    //    [Display(Name = "工時填報資料代碼")]
    //    public string RowUnid { get; set; }

    //    [Required]
    //    [Display(Name = "員工代號")]
    //    public string EmpID { get; set; }


    //    [Required]
    //    [Display(Name = "上班日")]
    //    public DateTime WorkingDate { get; set; }


    //    [Display(Name = "當日已填報工時")]
    //    public decimal totalWorkingHours { get; set; }


    //    [Display(Name = "應填工時")]
    //    public decimal FilledHours { get; set; }


    //    [Display(Name = "是否已填報完成")]
    //    public string IsFinish { get; set; }


    //    [Display(Name = "工時類別代碼")]
    //    public string TypeCode { get; set; }


    //    [Display(Name = "工時類別名稱")]
    //    public string TypeName { get; set; }


    //    [Display(Name = "Job代碼")]
    //    public string JobCode { get; set; }


    //    [Display(Name = "Job名稱")]
    //    public string JobName { get; set; }



    //    [Display(Name = "已填工時")]
    //    public decimal WorkingHours { get; set; }



    //    [Display(Name = "備註")]
    //    public string Note { get; set; }
    //}
    #endregion




















    // 員工萬年曆狀態列表
    public class WorkingDateEntity
    {
        DateTime m_WorkingDate;

        [Display(Name = "員工代號")]
        public string EmpID { get; set; }

        [Key]
        [JsonIgnore]
        [Display(Name = "工作日")]
        public DateTime WorkingDate
        {
            get
            {
                return m_WorkingDate;
            }
            set
            {
                m_WorkingDate = value;
            }
        }


        [Display(Name = "工作日")]
        [JsonPropertyName("workDate")]
        public string WorkDateString
        {
            get
            {
                return m_WorkingDate.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "填報工作日資料代碼")]
        public string RowUnid { get; set; }

        [Display(Name = "狀態訊息")]
        public string IsFinish { get; set; }
    }



    








    // 員工單筆工時明細
    public class WorkingHoursDetailEntity
    {
        [Required]
        [Display(Name = "工時填報資料代碼")]
        public string RowUnid { get; set; }

       

        [Display(Name = "工時類別代碼")]
        public string TypeCode { get; set; }

        [Display(Name = "Job代碼")]
        public string JobCode { get; set; }

        [Display(Name = "工時")]
        public decimal WorkingHours { get; set; }

        [Display(Name = "備註")]
        public string Note { get; set; }
    }










}
