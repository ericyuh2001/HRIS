using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS_WAMS_WebCoreAPI.Models;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace HRIS_WAMS_WebCoreAPI.Controllers
{
    [Route("api/v1/whs/[controller]")]
    [ApiController]
    public class DutyController : ControllerBase
    {
        private readonly HrisDbContext _context;

        public DutyController()
        {
        }







        /// <summary>
        /// 刪除填報資料
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     DELETE /api/v1/whs/duty
        ///     {
        ///         "RowUnid":"11111CF1-4449-4772-8601-58A478DF110B",
        ///         "DeletedBy":"001234"
        ///     }
        ///     </h2></pre>
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>     
        [HttpDelete]
        public async Task<ActionResult<DeleteWorkingHoursDetailEntity>> DeleteWorkingHoursDetail([FromBody]DeleteWorkingHoursDetailEntity empWorkingHoursDetailEntity)
        {


            string RowUnid = empWorkingHoursDetailEntity.RowUnid;
            string DeletedBy = empWorkingHoursDetailEntity.DeletedBy;


            string[] param = new[] {
                RowUnid
            };

            string sSQL = @"EXEC [whs].[usp_DeleteWorkingHoursDetail] 
                        @RowUnid = {0}";
            var MyHrisDB = new HrisDbContext();


            try
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
            }
            catch
            {
                return BadRequest();
            }




            return NoContent();


        }




        #region "閒置未使用API（已被合併替用）"



        ///// <summary>
        ///// 抓取員工首頁資訊
        ///// </summary>
        ///// <remarks>
        ///// <pre><h2>
        ///// 回傳範例
        /////     GET /api/v1/whs/duty/EmpID/692197/GetHomeInfoByEmp
        /////     {
        /////        "EmpID":"692197",
        /////        "EmpName":"李蕙芬",
        /////        "Sex":"2",
        /////        "message":"您目前尚無待辦事項須處理",
        /////        "WriteRedPoint":"1",
        /////        "SignRedPoint":"1",
        /////        "RoleID":"A",
        /////     }
        /////     </h2></pre>
        ///// </remarks>
        ///// <param name="EmpID" >員工代號</param>
        ///// <returns>傳回員工首頁資訊</returns>
        ///// <response code="201">代碼201說明描述</response>
        ///// <response code="400">代碼401說明描述</response>          
        //[HttpGet("EmpID/{EmpID}/GetHomeInfoByEmp")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IEnumerable<HomeInfoByEmpEntity> GetHomeInfoByEmp(string EmpID)
        //{
        //    string sSQL = "EXEC [whs].[usp_GetHomeInfoByEmp] {0}";
        //    var MyHrisDB = new HrisDbContext();
        //    var HomeInfoByEmpList = MyHrisDB.HomeInfoByEmpEntitys.FromSqlRaw(sSQL, EmpID);

        //    return HomeInfoByEmpList;
        //}



        ///// <summary>
        ///// 員工單日工時紀錄列表
        ///// </summary>
        ///// <remarks>
        ///// <pre><h2>
        ///// 回傳範例
        /////     GET /api/v1/whs/duty/RowUnid/00A05C2D-56D4-4C65-906B-B0A0B2B3A4BF/GetWorkingDateAllDetail
        /////     {
        /////        [{"rowUnid":"00A05C2D-56D4-4C65-906B-B0A0B2B3A4BF",
        /////        "EmpID":"309426",
        /////        "WorkingDate":"2020-04-07T00:00:00",
        /////        "totalWorkingHours":4.0,
        /////        "FilledHours":8.0,
        /////        "IsFinish":"未送出",
        /////        "TypeCode":"C01",
        /////        "TypeName":"CT/ICT",
        /////        "JobCode":"Bus_1",
        /////        "JobName":"市話語音",
        /////        "WorkingHours":3.5,
        /////        "Note":"TEST"}]
        /////     }
        /////     </h2></pre>
        ///// </remarks>
        ///// <param name="RowUnid" >工時填報資料代碼</param>
        ///// <returns>傳回工時填報資料</returns>
        ///// <response code="201">代碼201說明描述</response>
        ///// <response code="400">代碼401說明描述</response>          
        //[HttpGet("RowUnid/{RowUnid}/GetWorkingDateAllDetail")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IEnumerable<WorkingDateAllDetailEntity> GetWorkingDateAllDetail(string RowUnid)
        //{

        //    string sSQL = "EXEC [whs].[usp_GetWorkingDateAllDetail] {0}";
        //    var MyHrisDB = new HrisDbContext();
        //    var WorkingDateAllDetailList = MyHrisDB.WorkingDateAllDetailEntitys.FromSqlRaw(sSQL, RowUnid);

        //    return WorkingDateAllDetailList;
        //}




        ///// <summary>
        ///// 首頁待填報列表
        ///// </summary>
        ///// <remarks>
        ///// <pre><h2>
        ///// 回傳範例
        /////     GET /api/v1/whs/duty/EmpID/002688/GetAlterbyEmpID
        /////     {
        /////        {"EmpID":"848259",
        /////        "WorkingDate":"2020/04/07",
        /////        "status:","未送出"
        /////        },
        /////        {"EmpID":"848259",
        /////        "WorkingDate":"2020/04/08",
        /////        "status:","未送出"
        /////        },
        /////        {"EmpID":"848259",
        /////        "WorkingDate":"2020/04/09",
        /////        "status:","未送出"
        /////        }
        /////     }
        /////     </h2></pre>
        ///// </remarks>
        ///// <param name="EmpID" >員工代號</param>
        ///// <returns>傳回首頁待填報資料</returns>
        ///// <response code="201">代碼201說明描述</response>
        ///// <response code="400">代碼401說明描述</response>          
        //[HttpGet("EmpID/{EmpID}/GetAlterbyEmpID")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IEnumerable<AlterbyEmpIDEntity> GetAlterbyEmpID(string EmpID)
        //{

        //    string sSQL = "EXEC [whs].[usp_GetAlterbyEmpID] {0}";
        //    var MyHrisDB = new HrisDbContext();
        //    var AlterbyEmpIDInfo = MyHrisDB.AlterbyEmpIDEntitys.FromSqlRaw(sSQL, EmpID);


        //    return AlterbyEmpIDInfo;
        //}




        ///// <summary>
        ///// 抓取員工單日假單、加班單及判斷員工單日工時
        ///// </summary>
        ///// <remarks>
        ///// <pre><h2>
        ///// 回傳範例
        /////     GET /api/v1/whs/duty/EmpID/002688/WorkDate/20160223/GetEmpLeavebyWorkDate
        /////     {
        /////        {"EmpID": 002688,
        /////        "WorkDate": "2016-02-23 00:00:00.000",
        /////        "ApplyHours": 2,
        /////        "Reason":"新HRIS PRDS功能測試調整",
        /////        "type":"非專標案加班",
        /////        "IsProject":"N"
        /////        },
        /////        {"EmpID": 002688,
        /////        "WorkDate": "2016-02-23 00:00:00.000",
        /////        "ApplyHours": 1,
        /////        "Reason":"新HRIS PRDS功能測試調整",
        /////        "type":"非專標案加班",
        /////        "IsProject":"N"
        /////        }
        /////     }
        /////     </h2></pre>
        ///// </remarks>
        ///// <param name="EmpID" >員工代號</param>
        ///// <param name="WorkDateNo">上班日（格式yyyymmmdd）</param>
        ///// <returns>傳回員工單日工時</returns>
        ///// <response code="201">代碼201說明描述</response>
        ///// <response code="400">代碼401說明描述</response>          
        //[HttpGet("EmpID/{EmpID}/WorkDate/{WorkDateNo}/GetEmpLeavebyWorkDate")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IEnumerable<EmpLeavebyWorkDateEntity> GetEmpLeaveWorkDates(string EmpID, string WorkDateNo)
        //{
        //    string WorkDate = WorkDateNo.Substring(0, 4)
        //        + "/" + WorkDateNo.Substring(4, 2)
        //        + "/" + WorkDateNo.Substring(6, 2);
        //    string sSQL = "EXEC [whs].[usp_GetEmpLeavebyWorkDate] {0}, {1}";
        //    var MyHrisDB = new HrisDbContext();
        //    var EmpLeavebyWorkDateList = MyHrisDB.EmpLeavebyWorkDateEntitys.FromSqlRaw(sSQL, EmpID, WorkDate);

        //    return EmpLeavebyWorkDateList;
        //}
        #endregion














        /// <summary>
        /// 工時申請單
        /// 
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/EmpID/002688/WorkDate/20160223/GetEmpLeavebyWorkDate
        ///     {
        ///         "workDate":
        ///          {
        ///             "rowUnid":"00A05C2D-56D4-4C65-906B-B0A0B2B3A4BF",
        ///             "empID":"309426",
        ///             "workingDate":"2020-04-07",
        ///             "totalWorkingHours":4.0,
        ///             "filledHours":8.0,
        ///             "isFinish":"未送出"
        ///         },
        ///         "weekList":
        ///         [
        ///             {
        ///             "workingDate":"2020-04-06",
        ///             "isFinish":"未送出"
        ///             },
        ///             {
        ///             "workingdate":"2020-04-07",
        ///             "isFinish":"未送出"
        ///             },
        ///             {
        ///             "workingdate":"2020-04-08",
        ///             "isFinish":"已送出"
        ///             },
        ///             {
        ///             "workingdate":"2020-04-09",
        ///             "isFinish":"未送出"
        ///             },
        ///             {
        ///             "workingdate":"2020-04-10",
        ///             "isFinish":"未送出"
        ///             }
        ///         ],
        ///         "empLeaveList":
        ///         [
        ///             {
        ///             "EmpID": "002688",
        ///             "WorkDate": "2016-02-23",
        ///             "ApplyHours": 1,
        ///             "Reason":"休假",
        ///             "type":"非專標案加班",
        ///             "IsProject":"N"
        ///             },
        ///             {
        ///             "EmpID": "002688",
        ///             "WorkDate": "2016-02-23",
        ///             "ApplyHours": 2,
        ///             "Reason":"非專標案加班",
        ///             "type":"非專標案加班",
        ///             "IsProject":"N"
        ///             }
        ///         ],
        ///         "detailList":
        ///         [
        ///             {
        ///             "TypeCode":"B01",
        ///             "TypeName":"專標案(建置)",
        ///             "JobCode":"GA090054S",
        ///             "JobName":"國立暨南大學資工系教育行政資訊系統研發中心主機弱掃需求   ",
        ///             "WorkingHours":1,
        ///             "Note":"TEST1"
        ///             },
        ///             {
        ///             "TypeCode":"B01",
        ///             "TypeName":"專標案(建置)",
        ///             "JobCode":"DA090138S",
        ///             "JobName":"彰化銀行升速2路NG-SDH 100M數據專線     ",
        ///             "WorkingHours":2,
        ///             "Note":"TEST2"
        ///             }
        ///         ]
        ///     }
        ///     </h2></pre>
        /// </remarks>
        /// <param name="EmpID" >員工代號</param>
        /// <param name="WorkDateNo">上班日（格式yyyymmmdd）</param>
        /// <returns>傳回員工【單日填報統計】、【假單、加班單】、【員工週間申請狀態】、【單日填報工時明細】</returns>
        [HttpGet("EmpID/{EmpID}/WorkDate/{WorkDateNo}/GetEmpLeavebyWorkDate")]
        public EmpLeaveWithWorkDateDetailEntity GetEmpLeaveWorkDates(string EmpID, string WorkDateNo)
        {

            // 宣告回傳類別
            EmpLeaveWithWorkDateDetailEntity EmpLeaveWithWorkDateDetailInfo = new EmpLeaveWithWorkDateDetailEntity();


            // 初始化 ========================================================================
            // 取得傳入參數
            string WorkDate = WorkDateNo.Substring(0, 4)
                + "/" + WorkDateNo.Substring(4, 2)
                + "/" + WorkDateNo.Substring(6, 2);
            DateTime dteWorkdate;
            if (DateTime.TryParse(WorkDate, out dteWorkdate) == false)
            {
                return null;
            }

            
            int WeekStartOffset = (int)dteWorkdate.DayOfWeek;
            DateTime dteWeekStartDate = dteWorkdate.AddDays(-1 * WeekStartOffset);
            DateTime dteWeekStartEnd = dteWeekStartDate.AddDays(6);
            
           



            // 取得DB資料 ========================================================================

            // 實例化資料庫類別
            var MyHrisDB = new HrisDbContext();
            string sSQL;

            // 取得當日填報資料代碼
            string RowUnid = GetWorkingHoursRowUnidByWorkingDate(EmpID, dteWorkdate);

           

            // 取得：單日填報統計
            sSQL = "EXEC [whs].[usp_GetWorkingDateAllDetail] {0}";
            var EmpWorkdateListInfo = MyHrisDB.EmpWorkdateEntitys.FromSqlRaw(sSQL, RowUnid).ToList();
            if (EmpWorkdateListInfo != null && EmpWorkdateListInfo.Count > 0)
            {
                EmpLeaveWithWorkDateDetailInfo.WorkDate = EmpWorkdateListInfo.First();
            }
            


            // 取得：員工單日假單、加班單&判斷員工單日工時
            sSQL = "EXEC [whs].[usp_GetEmpLeavebyWorkDate] {0}, {1}";
            EmpLeaveWithWorkDateDetailInfo.EmpLeaveList = MyHrisDB.EmpLeavebyWorkDateEntitys.FromSqlRaw(sSQL, EmpID, WorkDate).ToList(); ;



            // 取得：員工週間申請狀態
            string sWeekStartDate = dteWeekStartDate.ToString("yyyy/MM/dd");
            string sWeekEndDate = dteWeekStartEnd.ToString("yyyy/MM/dd");
            sSQL = "EXEC [whs].[usp_GetWorkingDate] {0}, {1}, {2}";
            var EmpWorkingDateStatusListsInfo = MyHrisDB.EmpWorkingDateStatusLists.FromSqlRaw(sSQL, EmpID, sWeekStartDate, sWeekEndDate).ToList();
            EmpLeaveWithWorkDateDetailInfo.WeekList = EmpWorkingDateStatusListsInfo;


            // 取得：單日填報工時明細
            sSQL = "EXEC [whs].[usp_GetWorkingDateAllDetail_Detail] {0}";
            EmpLeaveWithWorkDateDetailInfo.DetailList = MyHrisDB.WorkingDateAllDetailEntitys.FromSqlRaw(sSQL, RowUnid).ToList();


            return EmpLeaveWithWorkDateDetailInfo;
        }




















        /// <summary>
        /// 抓取員工首頁資訊，包含首頁待填列表
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/EmpID/692197/GetHomeInfoByEmp
        ///     {
        ///         "home":
        ///          {
        ///             "EmpID":"692197",
        ///             "EmpName":"李蕙芬",
        ///             "Sex":"2",
        ///             "message":"您目前尚無待辦事項須處理",
        ///             "WriteRedPoint":"1",
        ///             "SignRedPoint":"1",
        ///             "RoleID":"A"
        ///         },
        ///         "alterList":
        ///         [
        ///             {
        ///             "empID":"848259",
        ///             "workingDate":"2020/04/07",
        ///             "status:":"未送出"
        ///             },
        ///             {
        ///             "empID":"848259",
        ///             "workingDate":"2020/04/08",
        ///             "status:":"未送出"
        ///             },
        ///             {
        ///             "empID":"848259",
        ///             "workingDate":"2020/04/09",
        ///             "status:":"未送出"
        ///             }
        ///         ],
        ///         "waitApproveList":
        ///         [
        ///             {
        ///             "empName":"賴x芬",
        ///             "flowID":"1020200413001997001                 ",
        ///             "message":"2020/3 W4 工時申報單"
        ///             },
        ///             {
        ///             "empName":"賴x芬",
        ///             "flowID":"1020200421001997001                 ",
        ///             "message":"2020/4 W3 工時申報單"
        ///             },
        ///             {
        ///             "empName":"王x義",
        ///             "flowID":"1020201535001988003                 ",
        ///             "message":"2020/4 W3 工時申報單"
        ///             }
        ///         ]
        ///     }
        ///     </h2></pre>
        ///     Last modified:  2020/05/06
        /// </remarks>
        /// <param name="EmpID" >員工代號</param>
        /// <returns>傳回員工首頁資訊</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("EmpID/{EmpID}/GetHomeInfoByEmp")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public HomeInfoWithAlterByEmpEntity GetHomeInfoByEmp(string EmpID)
        {

            // 取得 首頁待填報資訊
            string sSQL = "EXEC [whs].[usp_GetAlterbyEmpID] {0}";
            var MyHrisDB = new HrisDbContext();
            List<AlterbyEmpIDEntity> AlterbyEmpIDListInfo = MyHrisDB.AlterbyEmpIDEntitys.FromSqlRaw(sSQL, EmpID).ToList();


            // 抓取待批表單列表
            sSQL = "EXEC [whs].[usp_GetWaitApprove_WHS] {0}";
            List<WaitApproveEntity> WaitApproveListInfo = MyHrisDB.WaitApproveEntitys.FromSqlRaw(sSQL, EmpID).ToList();

            // 合併回傳資訊
            HomeInfoWithAlterByEmpEntity HomeInfoWithAlterByEmpInfo = new HomeInfoWithAlterByEmpEntity()
            {                
                alterListInfo = AlterbyEmpIDListInfo,
                waitApproveListInfo = WaitApproveListInfo
            };


            // 抓取員工首頁資訊
            sSQL = "EXEC [whs].[usp_GetHomeInfoByEmp] {0}";
            IEnumerable<HomeInfoByEmpEntity> HomeInfoByEmpInfo = MyHrisDB.HomeInfoByEmpEntitys.FromSqlRaw(sSQL, EmpID);
            if (HomeInfoByEmpInfo != null && HomeInfoByEmpInfo.Count() > 0)
            {
                HomeInfoWithAlterByEmpInfo.homeInfo = HomeInfoByEmpInfo.First();
            }


            return HomeInfoWithAlterByEmpInfo;
        }
















        /// <summary>
        /// 員工萬年曆狀態列表
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/EmpID/373409/StartDate/20200301/EndDate/20200331/GetWorkingDate
        ///     {
        ///        "empID":"373409",
        ///        "workingDate":"2020-03-01T00:00:00",
        ///        "isFinish":"9"
        ///     }
        ///     </h2></pre>
        /// </remarks>
        /// <param name="EmpID" >員工代號</param>
        /// <param name="StartDateNo">傳入日期區間：起始日期</param>
        /// <param name="EndDateNo">傳入日期區間：結束日期</param>
        /// <returns>傳回個人月曆資料清單資料表</returns>
        [HttpGet("EmpID/{EmpID}/StartDate/{StartDateNo}/EndDate/{EndDateNo}/GetWorkingDate")]
        public IEnumerable<WorkingDateEntity> GetWorkingDate(string EmpID, string StartDateNo, string EndDateNo)
        {

            // 傳入參數設定與檢查 ==========================================================
            string StartDate = StartDateNo.Substring(0, 4)
                + "/" + StartDateNo.Substring(4, 2)
                + "/" + StartDateNo.Substring(6, 2);

            string EndDate = EndDateNo.Substring(0, 4)
                + "/" + EndDateNo.Substring(4, 2)
                + "/" + EndDateNo.Substring(6, 2);
            if (DateTime.TryParse(StartDate, out DateTime dtStartDate) == false )
            {
                return null;
            }

            if (DateTime.TryParse(EndDate, out DateTime dtEndDate) == false)
            {
                return null;
            }


            // 取得資料 ==========================================================
            string sSQL = "EXEC [whs].[usp_GetWorkingDate] {0}, {1}, {2}";
            var MyHrisDB = new HrisDbContext();
            var WorkingDateList = MyHrisDB.WorkingDateEntitys.FromSqlRaw(sSQL, EmpID, StartDate, EndDate);

            return WorkingDateList;
        }













        ///// <summary>
        ///// 員工單筆工時明細
        ///// </summary>
        ///// <remarks>
        ///// <pre><h2>
        ///// 回傳範例
        /////     GET /api/v1/whs/duty/RowUnid/00A05C2D-56D4-4C65-906B-B0A0B2B3A4BF/TypeCode/C01/JobCode/Bus_1/GetWorkingHoursDetail
        /////     {
        /////         "RowUnid":"00A05C2D-56D4-4C65-906B-B0A0B2B3A4BF",
        /////         "TypeCode":"C01",
        /////         "JobCode":"Bus_1",
        /////         "WorkingHours":"3.5",
        /////         "Note":"Test"
        /////     }
        /////     </h2></pre>
        ///// </remarks>
        ///// <param name="RowUnid" >工時填報資料代碼</param>
        ///// <param name="TypeCode">Type代碼</param>
        ///// <param name="JobCode">Job代碼</param>
        ///// <returns>傳回員工單日可填報項目</returns>
        ///// <response code="201">代碼201說明描述</response>
        ///// <response code="400">代碼401說明描述</response>          
        //[HttpGet("RowUnid/{RowUnid}/TypeCode/{TypeCode}/JobCode/{JobCode}/GetWorkingHoursDetail")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IEnumerable<WorkingHoursDetailEntity> GetWorkingHoursDetail(string RowUnid, string TypeCode, string JobCode)
        //{

        //    string sSQL = "EXEC [whs].[usp_GetWorkingHoursDetail] {0}, {1}, {2}";
        //    var MyHrisDB = new HrisDbContext();
        //    var WorkingHoursDetailList = MyHrisDB.WorkingHoursDetailEntitys.FromSqlRaw(sSQL, RowUnid, TypeCode, JobCode);

        //    return WorkingHoursDetailList;
        //}






















        /// <summary>
        /// 輸入工時明細
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 傳入範例
        ///     POST /api/v1/whs/duty
        ///     {
        ///         "RowUnid":"00000CF1-4449-4772-8601-58A478DF110B",
        ///         "TypeCode":"C01",
        ///         "JobCode":"Bus_1",
        ///         "WorkingHours":1,
        ///         "Note":"Test_001",
        ///         "CreatedBy":"002688"
        ///     }
        ///     </h2></pre>
        /// </remarks>
        /// <returns>傳回建檔結果</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpPost]
        public async Task<ActionResult<EmpLeavebyWorkDateEntity>> InsertWorkingHoursDetail([FromBody]InsertWorkingHoursDetailEntity empWorkingHoursDetailEntity)
        {

            string RowUnid = empWorkingHoursDetailEntity.RowUnid;
            string TypeCode = empWorkingHoursDetailEntity.TypeCode;
            string JobCode = empWorkingHoursDetailEntity.JobCode;
            int WorkingHours = empWorkingHoursDetailEntity.WorkingHours;
            string Note = empWorkingHoursDetailEntity.Note;
            string CreatedBy = empWorkingHoursDetailEntity.CreatedBy;


            string[] param = new[] {
                RowUnid,
                TypeCode,
                JobCode,
                WorkingHours.ToString(),
                Note,
                CreatedBy
            };

            string sSQL = @"EXEC [whs].[usp_InsertWorkingHoursDetail] 
                        @RowUnid = {0}, 
                        @TypeCode = {1}, 
                        @JobCode = {2}, 
                        @WorkingHours = {3}, 
                        @Note = {4}, 
                        @CreatedBy = {5}";
            var MyHrisDB = new HrisDbContext();

            try
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
            }
            catch
            {
                return BadRequest();
            }



            return NoContent();

        }

















        /// <summary>
        /// 修改填報資料
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     PUT /api/v1/whs/duty
        ///     {
        ///         "RowUnid":"00000CF1-4449-4772-8601-58A478DF110B",
        ///         "TypeCode":"C01",
        ///         "JobCode":"Bus_1",
        ///         "WorkingHours":1,
        ///         "Note":"Test_001",
        ///         "CreatedBy":"002688"
        ///     }
        ///     </h2></pre>
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>     
        [HttpPut]
        public async Task<ActionResult<UpdateWorkingHoursDetailEntity>> UpdateWorkingHoursDetail([FromBody]UpdateWorkingHoursDetailEntity empWorkingHoursDetailEntity)
        {

            string RowUnid = empWorkingHoursDetailEntity.RowUnid;
            string TypeCode = empWorkingHoursDetailEntity.TypeCode;
            string JobCode = empWorkingHoursDetailEntity.JobCode;
            int WorkingHours = empWorkingHoursDetailEntity.WorkingHours;
            string Note = empWorkingHoursDetailEntity.Note;
            string CreatedBy = empWorkingHoursDetailEntity.CreatedBy;


            string[] param = new[] {
                RowUnid,
                TypeCode,
                JobCode,
                WorkingHours.ToString(),
                Note,
                CreatedBy
            };

            string sSQL = @"EXEC [whs].[usp_UpdateWorkingHoursDetail] 
                        @RowUnid = {0}, 
                        @TypeCode = {1}, 
                        @JobCode = {2}, 
                        @WorkingHours = {3}, 
                        @Note = {4}, 
                        @CreatedBy = {5}";
            var MyHrisDB = new HrisDbContext();

            try
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
            }
            catch
            {
                return BadRequest();
            }



            return NoContent();

        }




























        /// <summary>
        /// 取得RowUnid
        /// </summary>
        /// <param name="EmpID">員工代碼</param>
        /// <param name="WorkingDate">工作日</param>
        /// <remarks>
        /// 傳入EmpID, WorkingDate，查詢並轉換為單日填報RowUnid代碼
        /// </remarks>
        /// <returns></returns>
        protected string GetWorkingHoursRowUnidByWorkingDate(string EmpID, DateTime WorkingDate)
        {
            string RowUnid = null;

            // 查詢 [whs].[WorkingHours]
            var MyHrisDB = new HrisDbContext();
            var WorkingHoursQuerable = MyHrisDB.TB_WorkingHours
                .Where(b => b.EmpID == EmpID && b.WorkingDate == WorkingDate);


            if (WorkingHoursQuerable != null && WorkingHoursQuerable.Count() > 0)
            {
                WorkingHoursEntity WorkingHoursInfo = WorkingHoursQuerable.First();
                RowUnid = WorkingHoursInfo.RowUnid;
            }

            return RowUnid;
        }









    }
}
