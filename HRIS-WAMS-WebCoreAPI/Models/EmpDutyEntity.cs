using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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









    public class EmpLeaveWithWorkDateDetailEntity
    {
        public EmpWorkdateEntity workDate { get; set; }

        // 抓取員工單日假單、加班單&判斷員工單日工時
        public List<EmpLeavebyWorkDateEntity> EmpLeavebyWorkDateList { get; set; }

    }


    public class EmpWorkdateEntity
    {

    }


    // 員工單日假單、加班單&判斷員工單日工時
    public class EmpLeavebyWorkDateEntity
    {
        [Required]
        [Display(Name = "員工代碼")]
        public string EmpID { get; set; }

        [Required]
        [Display(Name = "上班日")]
        public DateTime WorkDate { get; set; }

        [Display(Name = "套用時數")]
        public int ApplyHours { get; set; }

        [Display(Name = "備註及說明")]
        public string Reason { get; set; }

        [Display(Name = "工時類型")]
        public string type { get; set; }

        [Display(Name = "專案註記")]
        public string IsProject { get; set; }

    }













    // 員工萬年曆狀態列表
    public class WorkingDateEntity
    {
        [Display(Name = "員工代號")]
        public string EmpID { get; set; }

        [Display(Name = "工作日")]
        public string WorkingDate { get; set; }

        [Display(Name = "填報工作日資料代碼")]
        public string RowUnid { get; set; }

        [Display(Name = "狀態訊息")]
        public string IsFinish { get; set; }
    }



    // 員工單日工時紀錄列表
    public class WorkingDateAllDetailEntity
    {
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
        public decimal totalWorkingHours { get; set; }


        [Display(Name = "應填工時")]
        public decimal FilledHours { get; set; }


        [Display(Name = "是否已填報完成")]
        public string IsFinish { get; set; }


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
