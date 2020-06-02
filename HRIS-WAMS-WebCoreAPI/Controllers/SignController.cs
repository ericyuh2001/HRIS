using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS_WAMS_WebCoreAPI.Models;
//using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Net;

namespace HRIS_WAMS_WebCoreAPI.Controllers
{
    [Route("api/v1/whs/[controller]")]
    [ApiController]
    public class SignController : ControllerBase
    {



        /// <summary>
        /// 抓取待批表單列表
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/sign/EmpID/002688/GetWaitApprove_WHS
        ///     {
        ///        {"EmpName":"賴x芬",
        ///        "FlowID":"1020200413001997001                 ",
        ///        "message":"2020/3 W4 工時申報單"
        ///        },
        ///        {"EmpName":"賴x芬",
        ///        "FlowID":"1020200421001997001                 ",
        ///        "message":"2020/4 W3 工時申報單"
        ///        }
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <param name="EmpID" >員工編號</param>
        /// <returns>傳回待批表單列表資料</returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>
        [HttpGet("EmpID/{EmpID}/GetWaitApprove_WHS")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public async Task<ActionResult<IEnumerable<WaitApproveEntity>>> GetWaitApprove_WHS(string EmpID)
        {
            List<WaitApproveEntity> WaitApproveListInfo = new List<WaitApproveEntity>();

            // 檢查傳入參數 ==========================================
            EmpID = EmpID.Trim();
            if (EmpID.Length != 6)
            {
                return StatusCode(401, WaitApproveListInfo);
            }



            // ========================================================================
            string sSQL = "EXEC [whs].[usp_GetWaitApprove_WHS] {0}";
            var MyHrisDB = new HrisDbContext();
            try
            {
                WaitApproveListInfo = 
                    await MyHrisDB.WaitApproveEntitys
                        .FromSqlRaw(sSQL, EmpID)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(501, WaitApproveListInfo);
            }
            

            return Ok(WaitApproveListInfo);
        }





        /// <summary>
        /// 主管單筆批核
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 傳入範例一（核准）
        ///     POST /api/v1/whs/sign
        ///     {
        ///         "flowID":"1020200413001997001                 ",
        ///         "signID":"692197",
        ///         "isApproval":"1",
        ///         "signLogDetailInfo":
        ///         [
        ///             {
        ///                 "rowUnid":"000434DC-222F-42B0-BBD3-C95DD8D40506",
        ///                 "note":""
        ///             },
        ///             {
        ///                 "rowUnid":"0004A24E-8391-48DA-8F20-9488A964335E",
        ///                 "note":""
        ///             },
        ///             {
        ///                 "rowUnid":"0006C7CE-3624-4A52-B803-1E9D5F032A63",
        ///                 "note":""
        ///             },
        ///             {
        ///                 "rowUnid":"00077DD0-FA9F-4566-9BA2-7CCBF93DF3FF",
        ///                 "note":""
        ///             }
        ///         ]
        ///     }
        ///     </h2><h2>
        /// 傳入範例二（退件）
        ///     POST /api/v1/whs/sign
        ///     {
        ///       "flowID": "1020200519002688004",
        ///       "signID": "692197",
        ///       "isApproval": "3",
        ///       "signLogDetailInfo": [
        ///         {
        ///           "rowUnid": "526635AE-31EA-4CDD-9FE2-0DB785038DC7",
        ///           "note": ""
        ///         },
        ///         {
        ///           "rowUnid": "5F3C8637-B728-4D07-8D3B-8C96DFEC2EC2",
        ///           "note": "5F3_第2筆退件測試_EC2"
        ///         },
        ///         {
        ///           "rowUnid": "9293C14E-4823-4639-8C07-004924F409FC",
        ///           "note": ""
        ///         },
        ///         {
        ///           "rowUnid": "BF2A140D-1DB1-4D0B-B828-9784D2323F89",
        ///           "note": "BF2__第4筆退件測試_F89"
        ///         },
        ///         {
        ///           "rowUnid": "D02BDF99-24BB-478A-9BAA-CFA631A69883",
        ///           "note": "D02__第5筆退件測試_883"
        ///         }
        ///       ]
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>          
        /// <response code="501">內部錯誤</response>          
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public async Task<IActionResult> SignByFlowID([FromBody] SingleSignActionBody SingleSignActionInfo)
        {
            string FlowID = SingleSignActionInfo.FlowID.Trim();
            string RowUnidArrayString = string.Empty;
            string SignID = SingleSignActionInfo.SignID;
            string SingRemark = string.Empty;
            string NoteArrayString = string.Empty;
            string IsFinish = "3";


            // 簽准：IsFinish = '1'；退回：IsFinish = '3'
            if (SingleSignActionInfo.IsApproval == "1")
                IsFinish = "1";

            if (FlowID == string.Empty
                || SignID == string.Empty
                || (SingleSignActionInfo.IsApproval == string.Empty))
                return StatusCode(401);


            int rowCount = 0;
            foreach (SignLogDetailEntity thisSignLogDetail in SingleSignActionInfo.SignLogDetailInfo)
            {
                string currentRowUnid = thisSignLogDetail.RowUnid.Trim();
                string currentSingRemark = thisSignLogDetail.Note.Trim().Replace(",", "，");

                if (rowCount == 0)
                {
                    RowUnidArrayString = currentRowUnid;
                    NoteArrayString = currentSingRemark;
                }
                else
                {
                    RowUnidArrayString  += "," + currentRowUnid;
                    NoteArrayString +=  "," + currentSingRemark;
                }

                rowCount ++ ;
            }



            // =====================================================================
            string[] param = new[] {
                FlowID,
                RowUnidArrayString,
                SignID,
                SingRemark,
                NoteArrayString,
                IsFinish
            };

            string sSQL = @"EXEC [whs].[usp_UpdatedProcessStatusBySign] 
                        @FlowID = {0}, 
                        @RowUnid = {1}, 
                        @SignID = {2}, 
                        @SingRemark = {3}, 
                        @Note = {4}, 
                        @IsFinish = {5},
                        @UpdatedBy = NULL";

            var MyHrisDB = new HrisDbContext();


            try
            {
                await MyHrisDB.Database.ExecuteSqlRawAsync(sSQL, param);
            }
            catch (Exception ex)
            {
                return StatusCode(501);
            }



            return Ok();
        }











        /// <summary>
        /// 主管週間工時待批列表
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/sign/EmpID/726124/StartDate/20200413/EndDate/20200419/GetWaitApproveDetail
        ///     {
        ///         "waitApproveStatic":
        ///             {
        ///                 "itemCount":9
        ///             },
        ///         "waitApproveList":
        ///         [
        ///             {
        ///                 "flowID":"1020200413001997001                 ",
        ///                 "empID":"024457",
        ///                 "empName":"高國華",
        ///                 "totalHour":40
        ///             },
        ///             {
        ///                 "flowID":"1020200413001997002                 ",
        ///                 "empID":"138559",
        ///                 "empName":"陳毓琦       ",
        ///                 "totalHour":40
        ///             },
        ///             {
        ///                 "flowID":"1020200413991997003                 ",
        ///                 "empID":"557783",
        ///                 "empName":"吳晉寶       ",
        ///                 "totalHour":40
        ///             },
        ///             {
        ///                 "flowID":"1020200413001868003                 ",
        ///                 "empID":"694751",
        ///                 "empName":"蔣孝澈       ",
        ///                 "totalHour":40
        ///             },
        ///             {
        ///                 "flowID":"102020062100199755                 ",
        ///                 "empID":"256475",
        ///                 "empName":"梁美珍       ",
        ///                 "totalHour":40
        ///             },
        ///             {
        ///                 "flowID":"102020023304297021                 ",
        ///                 "empID":"246541",
        ///                 "empName":"朱心誠       ",
        ///                 "totalHour":40
        ///             },
        ///             {
        ///                 "flowID":"102020040012370121                 ",
        ///                 "empID":"233465",
        ///                 "empName":"陳邦本       ",
        ///                 "totalHour":40
        ///             },
        ///             {
        ///                 "flowID":"102020045500199452                 ",
        ///                 "empID":"245315",
        ///                 "empName":"陳立文       ",
        ///                 "totalHour":40
        ///             },
        ///             {
        ///                 "flowID":"1020200234001455622                 ",
        ///                 "empID":"132345",
        ///                 "empName":"高石頭       ",
        ///                 "totalHour":40
        ///             }
        ///         ]
        ///     }
        ///     </h2></pre>
        /// </remarks>
        /// <param name="EmpID" >主管員工代號</param>
        /// <param name="StartDateNo">傳入日期區間：起始日期</param>
        /// <param name="EndDateNo">傳入日期區間：結束日期</param>
        /// <returns>傳回週間待批工時清單</returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>
        [HttpGet("EmpID/{EmpID}/StartDate/{StartDateNo}/EndDate/{EndDateNo}/GetWaitApproveDetail")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public async Task<ActionResult<WaitApproveStaticAndDetailEntity>> GetWaitApproveDetailByDateRange(string EmpID, string StartDateNo, string EndDateNo)
        {
            bool bolInValid = false;
            WaitApproveStaticAndDetailEntity WaitApproveStaticAndDetailInfo = new WaitApproveStaticAndDetailEntity();
            List<WaitApproveDetailEntity> WaitApproveDetailListInfo = null;
            ActionResult retResultWithStatus = null;

            DateTime dtStartDate = default(DateTime);
            DateTime dtEndDate = default(DateTime);
            // 待批案件筆數
            int WaitApproveDetailItemCount = 0;
            string sSQL = string.Empty;
            HrisDbContext MyHrisDB = new HrisDbContext();


            // 傳入參數設定與檢查 ==========================================================
            string StartDate = StartDateNo.Substring(0, 4)
                + "/" + StartDateNo.Substring(4, 2)
                + "/" + StartDateNo.Substring(6, 2);

            string EndDate = EndDateNo.Substring(0, 4)
                + "/" + EndDateNo.Substring(4, 2)
                + "/" + EndDateNo.Substring(6, 2);
            if (DateTime.TryParse(StartDate, out dtStartDate) == false)
            {
                bolInValid = true;
                retResultWithStatus = StatusCode(401, WaitApproveStaticAndDetailInfo);
            }

            if (DateTime.TryParse(EndDate, out dtEndDate) == false)
            {
                bolInValid = true;
                retResultWithStatus = StatusCode(401, WaitApproveStaticAndDetailInfo);
            }




            // 取得資料 ======================================================================
            if (!bolInValid)
            {
                sSQL = "EXEC [whs].[usp_GetWaitApproveDetail] {0}, {1}, {2}";
                MyHrisDB = new HrisDbContext();

                try
                {
                    WaitApproveDetailListInfo =
                        await MyHrisDB.WaitApproveDetailEntitys
                            .FromSqlRaw(sSQL, EmpID, dtStartDate, dtEndDate)
                            .ToListAsync();
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retResultWithStatus = StatusCode(501, WaitApproveStaticAndDetailInfo);
                }
            }
            
            


            // 回傳資料 ==============================================================
            if (bolInValid)
            {
                return retResultWithStatus;
            }
            else
            {
                if (WaitApproveDetailListInfo != null)
                    WaitApproveDetailItemCount = WaitApproveDetailListInfo.Count;


                WaitApproveStaticAndDetailInfo.WaitApproveStatic = new WaitApproveStaticEntity(WaitApproveDetailItemCount);
                WaitApproveStaticAndDetailInfo.WaitApproveList = WaitApproveDetailListInfo;
                
                return Ok(WaitApproveStaticAndDetailInfo);
            }
            
        }







        /// <summary>
        /// 批核工時-待批月曆及單日批核總計狀態
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/sign/EmpID/726124/StartDate/20200301/EndDate/20200331/GetWaitApproveDayStatic
        ///     [
        ///     {
        ///         "flowID": "1020200525002688001                 ",
        ///         "isFinish": "0",
        ///         "workDate": "2020-05-04",
        ///         "waitApproveItemCount": 1
        ///     },
        ///     {
        ///         "flowID": "1020200525002688001                 ",
        ///         "isFinish": "0",
        ///         "workDate": "2020-05-06",
        ///         "waitApproveItemCount": 1
        ///     }
        ///     ]
        ///     </h2></pre>
        /// </remarks>
        /// <param name="EmpID" >主管員工代號</param>
        /// <param name="StartDateNo">傳入日期區間：起始日期</param>
        /// <param name="EndDateNo">傳入日期區間：結束日期</param>
        /// <returns>傳回日期區間（通常是單月）內各日的批核總計狀態</returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>
        [HttpGet("EmpID/{EmpID}/StartDate/{StartDateNo}/EndDate/{EndDateNo}/GetWaitApproveDayStatic")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public async Task<ActionResult<List<WaitApproveDayStaticEntity>>> GetWaitApproveDayStaticByDateRange(string EmpID, string StartDateNo, string EndDateNo)
        {
            bool bolInValid = false;
            List<WaitApproveDayStaticEntity> WaitApproveDayStaticListInfo = new List<WaitApproveDayStaticEntity>();
            ActionResult retResultWithStatusCode = null;
            DateTime dtStartDate = default(DateTime);
            DateTime dtEndDate = default(DateTime);


            // 傳入參數設定與檢查 ==========================================================
            string StartDate = StartDateNo.Substring(0, 4)
                + "/" + StartDateNo.Substring(4, 2)
                + "/" + StartDateNo.Substring(6, 2);

            string EndDate = EndDateNo.Substring(0, 4)
                + "/" + EndDateNo.Substring(4, 2)
                + "/" + EndDateNo.Substring(6, 2);
            if (DateTime.TryParse(StartDate, out  dtStartDate) == false)
            {
                bolInValid = true;
                retResultWithStatusCode = StatusCode(401, WaitApproveDayStaticListInfo);
            }

            if (DateTime.TryParse(EndDate, out  dtEndDate) == false)
            {
                bolInValid = true;
                retResultWithStatusCode = StatusCode(401, WaitApproveDayStaticListInfo);
            }



            // 取得資料 ======================================================================
            if (!bolInValid)
            {
                string sSQL = "EXEC [whs].[usp_GetWaitApproveDayStatic] {0}, {1}, {2}";
                var MyHrisDB = new HrisDbContext();

                try
                {
                    WaitApproveDayStaticListInfo =
                        await MyHrisDB.WaitApproveDayStaticEntitys
                            .FromSqlRaw(sSQL, EmpID, dtStartDate, dtEndDate)
                            .ToListAsync();

                    retResultWithStatusCode = Ok(WaitApproveDayStaticListInfo);
                }
                catch (Exception ex)
                {
                    bolInValid = true;
                    retResultWithStatusCode = StatusCode(501, WaitApproveDayStaticListInfo);
                }
            }



            return retResultWithStatusCode;
        }














        /// <summary>
        /// 主管瀏覽單筆申請待批案件
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/sign/FlowID/1020200413001997001/GetApplicationDetail
        ///     {
        ///         "home":
        ///         {
        ///             "empName":"彭政閔"
        ///         },
        ///         "workdateList":
        ///         [
        ///             {
        ///                 "rowUnid":"00DALSGJ-56D4-4C65-906B-F5F4AA23A4BF",
        ///                 "workingDate":"2020-04-06",
        ///                 "workingHourDetailList":
        ///                 [
        ///                     {
        ///                          "typeCode":"B01",
        ///                          "typeName":"專標案(建置)",
        ///                          "jobCode":"GA090054S",
        ///                          "jobName":"國立暨南大學資工系教育行政資訊系統研發中心主機弱掃需求   ",
        ///                          "workingHours":1,
        ///                          "note":"TEST1"
        ///                     },
        ///                     {
        ///                          "typeCode":"B01",
        ///                          "typeName":"專標案(建置)",
        ///                          "jobCode":"DA090138S",
        ///                          "jobName":"彰化銀行升速2路NG-SDH 100M數據專線     ",
        ///                          "workingHours":2,
        ///                          "note":"TEST2"
        ///                     }
        ///                 ]
        ///             },
        ///             {
        ///                 "rowUnid":"00DF544D-6D76-4CAA-666B-B0A0B2B3A4BF",
        ///                 "workingDate":"2020-04-08",
        ///                 "workingHourDetailList":
        ///                 [
        ///                     {
        ///                          "typeCode":"S01",
        ///                          "typeName":"專標案(建置)",
        ///                          "jobCode":"GA0SSSSSS",
        ///                          "jobName":"國立SSSSSSS政資訊系統研發中心主機弱掃需求   ",
        ///                          "workingHours":1,
        ///                          "note":"TESTSSS1"
        ///                     },
        ///                     {
        ///                          "typeCode":"S02",
        ///                          "typeName":"專標案(建置)",
        ///                          "jobCode":"DA0SSS222S",
        ///                          "jobName":"SSS22銀行升速2路NG-SDH 100M數據專線     ",
        ///                          "workingHours":2,
        ///                          "note":"TESTS2"
        ///                     }
        ///                 ]
        ///             },
        ///             {
        ///                 "rowUnid":"00A05C2D-5DS4-4C65-556B-B54S4S5622BF",
        ///                 "workingDate":"2020-04-09",
        ///                 "workingHourDetailList":
        ///                 [
        ///                     {
        ///                          "typeCode":"S01",
        ///                          "typeName":"專標案(建置)",
        ///                          "jobCode":"GA0GGG11",
        ///                          "jobName":"國立GGGGG政資訊系統研發中心主機弱掃需求   ",
        ///                          "workingHours":1,
        ///                          "note":"TESTGGGG1"
        ///                     },
        ///                     {
        ///                          "typeCode":"S02",
        ///                          "typeName":"專標案(建置)",
        ///                          "jobCode":"DA0GGG22",
        ///                          "jobName":"GGGG22銀行升速2路NG-SDH 100M數據專線     ",
        ///                          "workingHours":2,
        ///                          "note":"TESTG2"
        ///                     }
        ///                 ]
        ///             },
        ///             {
        ///                 "rowUnid":"00D5252D-66D4-4ASD-333B-B0A498S5S4BF",
        ///                 "workingDate":"2020-04-10",
        ///                 "workingHourDetailList":
        ///                 [
        ///                     {
        ///                          "typeCode":"S01",
        ///                          "typeName":"專標案(建置)",
        ///                          "jobCode":"GA0HHH1",
        ///                          "jobName":"國立HHHHHH政資訊系統研發中心主機弱掃需求   ",
        ///                          "workingHours":1,
        ///                          "note":"TESTHHH1"
        ///                     },
        ///                     {
        ///                          "typeCode":"S02",
        ///                          "typeName":"專標案(建置)",
        ///                          "jobCode":"DA0HHHH2",
        ///                          "jobName":"HHH22銀行升速2路NG-SDH 100M數據專線     ",
        ///                          "workingHours":2,
        ///                          "note":"TESTH2"
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        ///     </h2></pre>
        /// </remarks>
        /// <param name="FlowID" >簽核申請案件代碼</param>
        /// <returns>傳回待批之申請案件明細內容，包含周間列表資訊</returns>
        /// <response code="400">傳入FlowID不正確或查無對應資料</response>
        [HttpGet("FlowID/{FlowID}/GetApplicationDetail")]
        public ActionResult<ApplicationDetailEntity> GetApplicationDetail(string FlowID)
        {
            // ===========================================
            ApplicationDetailEntity retApplicationDetailInfo = new ApplicationDetailEntity();

            List<WorkdateListEntity> retWorkdateList = new List<WorkdateListEntity>();



            // ============================================
            var MyHrisDB = new HrisDbContext();


            // 查詢ProcessStatus ====================================================
            // 申請案件基本資料
            ProcessStatusEntity ProcessStatusInfo = MyHrisDB.TB_ProcessStatusEntitys
                     .Where(b => b.FlowID == FlowID && b.IsDeleted == "0").FirstOrDefault();
            if (ProcessStatusInfo == null)
                return NotFound(retApplicationDetailInfo);

            string EmpID = ProcessStatusInfo.ApplyID;
            string EmpName = MyHrisDB.TB_Employees
                .Where(e => e.EmpID == EmpID).Select(e => e.EmpName.Trim()).FirstOrDefault();

            retApplicationDetailInfo.Home = 
                new ApplicationDetailHomeEntity() { EmpName = EmpName };


            // 查詢ProcessStatusDetail ====================================================
            // 申請案件明細資料
            List<ProcessStatusDetailEntity> ProcessStatusDetailListInfo = 
                MyHrisDB.TB_ProcessStatusDetailEntitys
                .Where(p => p.FlowID == FlowID).ToList();

            foreach (string currentRowUnid in ProcessStatusDetailListInfo
                .Select(p => p.RowUnid.Trim()))
            {
                WorkingHoursEntity WorkingHoursEntityInfo = 
                    MyHrisDB.TB_WorkingHours
                    .Where(w => w.RowUnid == currentRowUnid && w.IsDelete == "0")
                    .FirstOrDefault();

                if (WorkingHoursEntityInfo == null)
                    return NotFound(retApplicationDetailInfo);


                List<WorkingHoursDetailWithJobNameEntity> WorkingHoursDetailWithJobNameListInfo =
                    (from currentvwWorkingHoursDetailInfo in MyHrisDB.VW_vwWorkingHoursDetailEntitys
                     where currentvwWorkingHoursDetailInfo.RowUnid == currentRowUnid 
                     select new WorkingHoursDetailWithJobNameEntity
                     {
                         TypeCode = currentvwWorkingHoursDetailInfo.TypeCode,
                         JobCode = currentvwWorkingHoursDetailInfo.JobCode,
                         JobName = currentvwWorkingHoursDetailInfo.JobName,
                         WorkingHours = currentvwWorkingHoursDetailInfo.WorkingHours,
                         Note = currentvwWorkingHoursDetailInfo.Note
                     }).ToList();


                WorkdateListEntity WorkdateListInfo = new WorkdateListEntity();
                WorkdateListInfo.RowUnid = WorkingHoursEntityInfo.RowUnid;
                WorkdateListInfo.WorkingDate = WorkingHoursEntityInfo.WorkingDate.ToString("yyyy-MM-dd");
                WorkdateListInfo.WorkingHourDetailList = WorkingHoursDetailWithJobNameListInfo;

                retWorkdateList.Add(WorkdateListInfo);
            }

            retApplicationDetailInfo.WorkdateList = retWorkdateList;


            return Ok(retApplicationDetailInfo);
        }













        /// <summary>
        /// 主管單筆批核
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     PUT /api/v1/whs/sign
        ///     Sample 1
        ///     {
        ///         "FlowID":"1020200421001997001",
        ///         "SignID":"002688",
        ///         "SingRemark":"簽核測試_簽准_test001",
        ///         "IsFinish":"1",
        ///         "UpdatedBy":"002688",
        ///     }
        ///     </h2></pre>
        ///     
        ///     IsFinish =1 簽准
        ///     IsFinish = 3 簽退
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">操作完成</response>        
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public IActionResult UpdatedProcessStatusBySign([FromBody] UpdatedProcessStatusBySignEntity UpdatedProcessStatusInfo)
        {
            string FlowID = UpdatedProcessStatusInfo.FlowID.Trim();
            string SignID = UpdatedProcessStatusInfo.SignID.Trim();
            string SingRemark = UpdatedProcessStatusInfo.SingRemark.Trim();
            string IsFinish = UpdatedProcessStatusInfo.IsFinish.Trim();
            string UpdatedBy = UpdatedProcessStatusInfo.UpdatedBy.Trim();


            if (FlowID == string.Empty 
                || SignID == string.Empty 
                || IsFinish == string.Empty
                || UpdatedBy == string.Empty)
            {
                return StatusCode(401);
            }


            string[] param = new[] {
                FlowID,
                SignID,
                SingRemark,
                IsFinish,
                UpdatedBy
            };

            string sSQL = @"EXEC [whs].[usp_UpdatedProcessStatusBySign] 
                        @FlowID = {0}, 
                        @SignID = {1}, 
                        @SingRemark = {2}, 
                        @IsFinish = {3}, 
                        @UpdatedBy = {4}";
            var MyHrisDB = new HrisDbContext();

            try
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
            }
            catch (Exception ex)
            {
                return StatusCode(501);
            }



            return Ok();

        }













    }
}