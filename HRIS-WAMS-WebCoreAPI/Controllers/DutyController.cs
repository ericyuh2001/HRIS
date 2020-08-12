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
using System.Net;

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
        /// 回傳範例
        /// 
        ///     DELETE /api/v1/whs/duty
        ///     {
        ///         "RowUnid":"9C821CF6-535F-44D2-96D1-41D61B8BDB1C",
        ///         "TypeCode":"B01",
        ///         "JobCode":"Bus_83"
        ///     }
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">操作完成</response>        
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>     
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public IActionResult  DeleteWorkingHoursDetail([FromBody]DeleteWorkingHoursDetailHandleEntity empWorkingHoursDetailHandleInfo)
        {
            // =======================================================
            string RowUnid = empWorkingHoursDetailHandleInfo.RowUnid.Trim();
            string TypeCode = empWorkingHoursDetailHandleInfo.TypeCode.Trim();
            string JobCode = empWorkingHoursDetailHandleInfo.JobCode.Trim();


            string[] arySqlParam = new[] {
                RowUnid, TypeCode, JobCode
            };

            if (RowUnid == string.Empty || TypeCode == string.Empty || JobCode == string.Empty)
            {
                return StatusCode(401);
            }


            // ===================================================================
            string sSQL = @"EXEC [whs].[usp_DeleteWorkingHoursDetail] 
                        @RowUnid = {0}, 
                        @TypeCode = {1}, 
                        @JobCode = {2}";
            var MyHrisDB = new HrisDbContext();

            try
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, arySqlParam);
            }
            catch
            {
                return StatusCode(501);
            }


            return Ok();
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
        /// </summary>
        /// <remarks>
        /// 
        /// 回傳範例
        /// 
        ///     GET /api/v1/whs/duty/EmpID/002688/WorkDate/20200415/GetEmpLeavebyWorkDate
        ///     {
        ///       "workDate": {
        ///         "rowUnid": "67D6D805-E17E-4BC0-A222-041EC90317C4",
        ///         "empID": "002688",
        ///         "workingDate": "2020-04-15",
        ///         "totalWorkingHours": 8,
        ///         "filledHours": 8,
        ///         "isFinish": "已退回",
        ///         "signRemark": "aaa"
        ///       },
        ///       "weekList": [
        ///         {
        ///           "workingDate": "2020-04-14",
        ///           "isFinish": "已送出"
        ///         },
        ///         {
        ///           "workingDate": "2020-04-15",
        ///           "isFinish": "已退回"
        ///         },
        ///         {
        ///           "workingDate": "2020-04-13",
        ///           "isFinish": "已送出"
        ///         },
        ///         {
        ///           "workingDate": "2020-04-17",
        ///           "isFinish": "已退回"
        ///         },
        ///         {
        ///           "workingDate": "2020-04-16",
        ///           "isFinish": "已送出"
        ///         }
        ///       ],
        ///       "empLeaveList": [],
        ///       "detailList": [
        ///         {
        ///           "rowUnid": "67D6D805-E17E-4BC0-A222-041EC90317C4",
        ///           "typeCode": "C01",
        ///           "typeName": "CT/ICT",
        ///           "jobCode": "Bus_1",
        ///           "jobName": null,
        ///           "workingHours": 8,
        ///           "note": ""
        ///         }
        ///       ]
        ///     }
        ///     
        /// </remarks>
        /// <param name="EmpID" >員工代號</param>
        /// <param name="WorkDateNo">上班日（格式yyyymmdd）</param>
        /// <returns>傳回員工【單日填報統計】、【假單、加班單】、【員工週間申請狀態】、【單日填報工時明細】</returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>
        /// <response code="404">內部資料有誤</response>
        /// <response code="501">內部錯誤</response>
        [HttpGet("EmpID/{EmpID}/WorkDate/{WorkDateNo}/GetEmpLeavebyWorkDate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(501)]
        public async Task<ActionResult<EmpLeaveWithWorkDateDetailEntity>> GetEmpLeaveWorkDates(string EmpID, string WorkDateNo)
        {
            bool bolInValid = false;                            // 紀錄程序是否錯誤      
            string strRetMsg = string.Empty;                    // 紀錄程序錯誤訊息
            ActionResult retStatusCode = null;                  // 回傳StatusCode

            DateTime dteWorkdate = default(DateTime);           // 傳入日期參數
            DateTime dteWeekStartDate = default(DateTime);      // 當週起始日期
            DateTime dteWeekStartEnd = default(DateTime);       // 當週結束日期

            HrisDbContext MyHrisDB = null;
            string sSQL = string.Empty;
            string RowUnid = string.Empty;                      // 傳入日期所對應之RowUnid

            // 宣告回傳類別
            EmpLeaveWithWorkDateDetailEntity EmpLeaveWithWorkDateDetailInfo = new EmpLeaveWithWorkDateDetailEntity();


            // 初始化 ========================================================================
            // 取得傳入參數
            string WorkDate = WorkDateNo.Substring(0, 4)
                + "/" + WorkDateNo.Substring(4, 2)
                + "/" + WorkDateNo.Substring(6, 2);
            
            if (DateTime.TryParse(WorkDate, out dteWorkdate) == false)
            {
                bolInValid = true;
                retStatusCode = StatusCode(401);
            }
            else
            {
                int WeekStartOffset = (int)dteWorkdate.DayOfWeek;
                dteWeekStartDate = dteWorkdate.AddDays(-1 * WeekStartOffset);
                dteWeekStartEnd = dteWeekStartDate.AddDays(6);
            }

            
                       
           


            // 取得DB資料 ========================================================================
            if (!bolInValid)
            {
                // 實例化資料庫類別
                MyHrisDB = new HrisDbContext();

                // 取得當日填報資料代碼
                RowUnid = GetWorkingHoursRowUnidByWorkingDate(EmpID, dteWorkdate);
                if (RowUnid == null || RowUnid.Trim() == string.Empty)
                {
                    bolInValid = true;
                    retStatusCode = NotFound(EmpLeaveWithWorkDateDetailInfo);
                }
            }



            // 取得：員工單日假單、加班單&判斷員工單日工時 ======================================
            // 優先執行：此SP將更新 [WorkingHours].[FilledHours]
            // 以同步方式呼叫
            if (!bolInValid)
            {
                sSQL = "EXEC [whs].[usp_GetEmpLeavebyWorkDate] {0}, {1}";
                try
                {
                    EmpLeaveWithWorkDateDetailInfo.EmpLeaveList =
                        MyHrisDB.EmpLeavebyWorkDateEntitys
                            .FromSqlRaw(sSQL, EmpID, WorkDate)
                            .ToList();
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retStatusCode = StatusCode(501, EmpLeaveWithWorkDateDetailInfo);
                }
            }



            // 取得：單日填報統計 ===============================================================
            if (!bolInValid)
            {
                sSQL = "EXEC [whs].[usp_GetWorkingDateAllDetail] {0}";
                try
                {
                    var EmpWorkdateListInfo = 
                        await MyHrisDB.EmpWorkdateEntitys
                            .FromSqlRaw(sSQL, RowUnid)
                            .ToListAsync();

                    if (EmpWorkdateListInfo != null && EmpWorkdateListInfo.Count > 0)
                    {
                        EmpLeaveWithWorkDateDetailInfo.WorkDate = EmpWorkdateListInfo.First();
                    }
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retStatusCode = StatusCode(501, EmpLeaveWithWorkDateDetailInfo);
                }
            }




                       
            



            // 取得：員工週間申請狀態 ============================================================
            if (!bolInValid)
            {
                string sWeekStartDate = dteWeekStartDate.ToString("yyyy/MM/dd");
                string sWeekEndDate = dteWeekStartEnd.ToString("yyyy/MM/dd");
                sSQL = "EXEC [whs].[usp_GetWorkingDate] {0}, {1}, {2}";
                try
                {
                    var EmpWorkingDateStatusListsInfo =
                        await MyHrisDB.EmpWorkingDateStatusLists
                            .FromSqlRaw(sSQL, EmpID, sWeekStartDate, sWeekEndDate)
                            .ToListAsync();
                    EmpLeaveWithWorkDateDetailInfo.WeekList = EmpWorkingDateStatusListsInfo;
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retStatusCode = StatusCode(501, EmpLeaveWithWorkDateDetailInfo);
                }
            }
            
            



            // 取得：單日填報工時明細 =======================================================
            if (!bolInValid)
            {
                sSQL = "EXEC [whs].[usp_GetWorkingDateAllDetail_Detail] {0}";
                try
                {
                    EmpLeaveWithWorkDateDetailInfo.DetailList =
                        await MyHrisDB.WorkingDateAllDetailEntitys
                            .FromSqlRaw(sSQL, RowUnid)
                            .ToListAsync();

                    retStatusCode = Ok(EmpLeaveWithWorkDateDetailInfo);
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retStatusCode = StatusCode(501, EmpLeaveWithWorkDateDetailInfo);
                }                
            }



            // API 回傳 ==============================================================
            return retStatusCode;
        }




















        /// <summary>
        /// 抓取員工首頁資訊，包含首頁待填列表
        /// </summary>
        /// <remarks>
        /// 回傳範例
        /// 
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
        ///             "workingDate":"2020/04/07 工時填報單",
        ///             "status":"未送出",
        ///             "linkDate":"2020/04/07"
        ///             },
        ///             {
        ///             "empID":"848259",
        ///             "workingDate":"2020/04/08 工時填報單",
        ///             "status":"未送出",
        ///             "linkDate":"2020/04/07"
        ///             },
        ///             {
        ///             "empID":"848259",
        ///             "workingDate":"2020/04/09 工時填報單",
        ///             "status":"未送出",
        ///             "linkDate":"2020/04/07"
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
        ///     
        /// </remarks>
        /// <param name="EmpID" >員工代號</param>
        /// <returns>傳回員工首頁資訊</returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>      
        [HttpGet("EmpID/{EmpID}/GetHomeInfoByEmp")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public async Task<ActionResult<HomeInfoWithAlterByEmpEntity>> GetHomeInfoByEmp(string EmpID)
        {
            const string ProcedureID = "GetHomeInfoByEmp";

            bool bolInValid = false;
            HomeInfoWithAlterByEmpEntity HomeInfoWithAlterByEmpInfo = new HomeInfoWithAlterByEmpEntity();
            ActionResult retResultWithStatus = null;
            string sSQL = string.Empty;
            HrisDbContext MyHrisDB = null;

            List<AlterbyEmpIDEntity> AlterbyEmpIDListInfo = null;
            List<WaitApproveEntity> WaitApproveListInfo = null;

            // 取得 首頁待填報資訊
            sSQL = "EXEC [whs].[usp_GetAlterbyEmpID] {0}";
            MyHrisDB = new HrisDbContext();

            try
            {
                AlterbyEmpIDListInfo = 
                    await MyHrisDB.AlterbyEmpIDEntitys
                        .FromSqlRaw(sSQL, EmpID)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                bolInValid = true;
                retResultWithStatus = StatusCode(501, HomeInfoWithAlterByEmpInfo);


                var ael = new ApiExceptionLog
                {
                    ProcedureID = ProcedureID,
                    EmpID = EmpID,
                    ProcedureStepDescription = "EXEC whs.[usp_GetAlterbyEmpID]"
                };
                await ApiExceptionLogHelper.AddLogSync( MyHrisDB, ex, ael);
            }
            


            // 抓取待批表單列表 =========================================================
            if (!bolInValid)
            {
                sSQL = "EXEC [whs].[usp_GetWaitApprove_WHS] {0}";
                try
                {
                    WaitApproveListInfo = 
                        await MyHrisDB.WaitApproveEntitys
                            .FromSqlRaw(sSQL, EmpID)
                            .ToListAsync();
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retResultWithStatus = StatusCode(501, HomeInfoWithAlterByEmpInfo);

                    var ael = new ApiExceptionLog
                    {
                        ProcedureID = ProcedureID,
                        EmpID = EmpID,
                        ProcedureStepDescription = "[whs].[usp_GetWaitApprove_WHS]"
                    };
                    await ApiExceptionLogHelper.AddLogSync( MyHrisDB, ex, ael);
                }
            }




            // 合併回傳資訊 ==============================================================
            if (!bolInValid)
            {
                HomeInfoWithAlterByEmpInfo = new HomeInfoWithAlterByEmpEntity()
                {
                    alterListInfo = AlterbyEmpIDListInfo,
                    waitApproveListInfo = WaitApproveListInfo
                };
            }
            


            // 抓取員工首頁資訊 =========================================================
            if (!bolInValid)
            {
                sSQL = "EXEC [whs].[usp_GetHomeInfoByEmp] {0}";
                try
                {
                    IEnumerable<HomeInfoByEmpEntity> HomeInfoByEmpInfo = 
                        MyHrisDB.HomeInfoByEmpEntitys
                            .FromSqlRaw(sSQL, EmpID)
                            .AsEnumerable();

                    HomeInfoWithAlterByEmpInfo.homeInfo = HomeInfoByEmpInfo?.FirstOrDefault();
                    retResultWithStatus = Ok(HomeInfoWithAlterByEmpInfo);
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retResultWithStatus = StatusCode(501, HomeInfoWithAlterByEmpInfo);

                    var ael = new ApiExceptionLog
                    {
                        ProcedureID = ProcedureID,
                        EmpID = EmpID,
                        ProcedureStepDescription = "[whs].[usp_GetHomeInfoByEmp]"
                    };
                    await ApiExceptionLogHelper.AddLogSync( MyHrisDB, ex, ael);
                }
            }



            return retResultWithStatus;
        }
















        /// <summary>
        /// 員工萬年曆狀態列表
        /// </summary>
        /// <remarks>
        /// 回傳範例
        /// 
        ///     GET /api/v1/whs/duty/EmpID/373409/StartDate/20200301/EndDate/20200331/GetWorkingDate
        ///     {
        ///        "empID":"373409",
        ///        "workingDate":"2020-03-01",
        ///        "isFinish":"9"
        ///     }
        /// </remarks>
        /// <param name="EmpID" >員工代號</param>
        /// <param name="StartDateNo">傳入日期區間：起始日期</param>
        /// <param name="EndDateNo">傳入日期區間：結束日期</param>
        /// <returns>傳回個人月曆資料清單資料表</returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>        
        [HttpGet("EmpID/{EmpID}/StartDate/{StartDateNo}/EndDate/{EndDateNo}/GetWorkingDate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public async Task<ActionResult<List<WorkingDateEntity>>> GetWorkingDate(string EmpID, string StartDateNo, string EndDateNo)
        {
            bool bolInValid = false;            
            List<WorkingDateEntity> WorkingDateList = new List<WorkingDateEntity>();
            ActionResult retStatusResult = null;


            // 傳入參數設定與檢查 ==========================================================
            string StartDate = StartDateNo.Substring(0, 4)
                + "/" + StartDateNo.Substring(4, 2)
                + "/" + StartDateNo.Substring(6, 2);

            string EndDate = EndDateNo.Substring(0, 4)
                + "/" + EndDateNo.Substring(4, 2)
                + "/" + EndDateNo.Substring(6, 2);
            if (DateTime.TryParse(StartDate, out _) == false )
            {
                bolInValid = true;
                retStatusResult = StatusCode(401, WorkingDateList);
            }

            if (DateTime.TryParse(EndDate, out _) == false)
            {
                bolInValid = true;
                retStatusResult = StatusCode(401, WorkingDateList);
            }





            // 取得資料 ==========================================================
            if (!bolInValid)
            {
                string sSQL = "EXEC [whs].[usp_GetWorkingDate] {0}, {1}, {2}";
                var MyHrisDB = new HrisDbContext();

                try
                {
                    WorkingDateList =
                        await MyHrisDB.WorkingDateEntitys
                            .FromSqlRaw(sSQL, EmpID, StartDate, EndDate)
                            .ToListAsync();
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retStatusResult = StatusCode(501, WorkingDateList);
                }
                
            }
            
         


            if (bolInValid)
            {
                return retStatusResult;
            }
            else
            {
                return Ok(WorkingDateList);
            }
            
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
        /// 傳入範例
        /// 
        ///     POST /api/v1/whs/duty
        ///     Sample 1
        ///     {
        ///         "RowUnid":"00000CF1-4449-4772-8601-58A478DF110B",
        ///         "TypeCode":"C01",
        ///         "JobCode":"Bus_1",
        ///         "WorkingHours":1,
        ///         "Note":"Test_001",
        ///         "CreatedBy":"002688"
        ///     }
        ///     
        ///     Sample 2
        ///     {
        ///         "RowUnid":"0006C7CE-3624-4A52-B803-1E9D5F032A63",
        ///         "TypeCode":"J04",
        ///         "JobCode":"J04_99_1_1",
        ///         "WorkingHours":2,
        ///         "Note":"Test_260169_J04_J04_99_1_1",
        ///         "CreatedBy":"260169"
        ///     }
        ///     
        ///     
        /// 回傳範例
        /// 
        ///    範例1：送出完成
        ///    {
        ///         "message": "送出完成!!"
        ///     }
        ///     
        ///     範例2：不可超出應填時數
        ///     {
        ///         "message": "不可超出應填時數"
        ///     }
        ///     
        ///     範例3：本日已有相同資料
        ///     {
        ///         "message": "本日已有相同資料"
        ///     }
        /// 
        /// </remarks>
        /// <returns>傳回建檔結果</returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public ActionResult<InsertWorkingHoursDetailReturnEntity> InsertWorkingHoursDetail([FromBody]InsertWorkingHoursDetailEntity empWorkingHoursDetailEntity)
        {
            InsertWorkingHoursDetailReturnEntity retInsertWorkingHoursDetailReturnInfo= null;

            string RowUnid = empWorkingHoursDetailEntity.RowUnid;
            string TypeCode = empWorkingHoursDetailEntity.TypeCode;
            string JobCode = empWorkingHoursDetailEntity.JobCode;
            decimal WorkingHours = empWorkingHoursDetailEntity.WorkingHours;
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
                var returnMessageList =  
                    MyHrisDB.InsertWorkingHoursDetailReturnEntitys.FromSqlRaw(sSQL, param).ToList();

                if (returnMessageList != null && returnMessageList.Count > 0)
                    retInsertWorkingHoursDetailReturnInfo = returnMessageList[0];
            }
            catch
            {
                //return internalser (retInsertWorkingHoursDetailReturnInfo);
                return StatusCode(501);
            }


           

            return Ok(retInsertWorkingHoursDetailReturnInfo);
        }

















        /// <summary>
        /// 修改填報資料
        /// </summary>
        /// <remarks>
        /// 
        /// 回傳範例
        /// 
        ///     PUT /api/v1/whs/duty
        ///     Sample 1
        ///     {
        ///         "RowUnid":"00000CF1-4449-4772-8601-58A478DF110B",
        ///         "TypeCode":"C01",
        ///         "JobCode":"Bus_1",
        ///         "WorkingHours":1,
        ///         "Note":"Test_001",
        ///         "CreatedBy":"002688"
        ///     }
        ///     
        ///     Sample 2
        ///     {
        ///         "RowUnid":"0006C7CE-3624-4A52-B803-1E9D5F032A63",
        ///         "TypeCode":"J04",
        ///         "JobCode":"J04_99_1_1",
        ///         "WorkingHours":3,
        ///         "Note":"Test_260169_J04_J04_99_1_1_3",
        ///         "CreatedBy":"Tester"
        ///     }
        ///     
        /// 回傳範例
        /// 
        ///     範例1：更新完成
        ///     {
        ///         "message": "送出完成!!"
        ///     }
        ///     
        ///     範例2：不可超出應填時數
        ///     {
        ///         "message": "不可超出應填時數"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>     
        /// <response code="501">內部錯誤</response>     
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public  ActionResult<UpdateWorkingHoursDetailReturnEntity> UpdateWorkingHoursDetail([FromBody]UpdateWorkingHoursDetailEntity empWorkingHoursDetailEntity)
        {
            UpdateWorkingHoursDetailReturnEntity retUpdateWorkingHoursDetailReturnInfo = null;

            string RowUnid = empWorkingHoursDetailEntity.RowUnid;
            string TypeCode = empWorkingHoursDetailEntity.TypeCode;
            string JobCode = empWorkingHoursDetailEntity.JobCode;
            decimal WorkingHours = empWorkingHoursDetailEntity.WorkingHours;
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
                var varUpdateWorkingHoursDetailReturn =
                    MyHrisDB.UpdateWorkingHoursDetailReturnEntitys.FromSqlRaw(sSQL,param)
                    .ToList();

                if (varUpdateWorkingHoursDetailReturn != null && varUpdateWorkingHoursDetailReturn.Count > 0)
                    retUpdateWorkingHoursDetailReturnInfo = varUpdateWorkingHoursDetailReturn[0];

            }
            catch
            {
                return StatusCode(501, retUpdateWorkingHoursDetailReturnInfo);
            }



            return Ok(retUpdateWorkingHoursDetailReturnInfo);
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
                WorkingHoursEntity WorkingHoursInfo = WorkingHoursQuerable.FirstOrDefault();
                RowUnid = WorkingHoursInfo?.RowUnid;
            }

            return RowUnid;
        }









    }
}
