﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS_WAMS_WebCoreAPI.Models;




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
        [HttpGet("EmpID/{EmpID}/GetWaitApprove_WHS")]
        public IEnumerable<WaitApproveEntity> GetWaitApprove_WHS(string EmpID)
        {

            string sSQL = "EXEC [whs].[usp_GetWaitApprove_WHS] {0}";
            var MyHrisDB = new HrisDbContext();
            var WaitApproveInfo = MyHrisDB.WaitApproveEntitys.FromSqlRaw(sSQL, EmpID);

            return WaitApproveInfo;
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
        ///         "signLogList":
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
        ///         "flowID":"1020200413001997002                 ",
        ///         "signID":"692197",
        ///         "isApproval":"0"
        ///         "signLogDetailInfo":[
        ///         [
        ///             {
        ///                 "rowUnid":"000783F3-A85B-4704-B937-A69F638C5DD1",
        ///                 "note":""
        ///             },
        ///             {
        ///                 "rowUnid":"000CD695-E48E-4098-AF2C-02F3A68F10FC",
        ///                 "note":"當日填報有誤請再確認專案工作內容"
        ///             },
        ///             {
        ///                 "rowUnid":"000E6A93-C1D6-4A89-AACA-B7D18CD439AE",
        ///                 "note":""
        ///             },
        ///             {
        ///                 "rowUnid":"0010A82C-4CA7-491B-90F9-5D04B45EDDC3",
        ///                 "note":""
        ///             }
        ///         ]
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SignByFlowID([FromBody]SingleSignActionBody SingleSignActionInfo)
        {
            
            string IsApproval = SingleSignActionInfo.IsApproval;
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
        [HttpGet("EmpID/{EmpID}/StartDate/{StartDateNo}/EndDate/{EndDateNo}/GetWaitApproveDetail")]
        public WaitApproveStaticAndDetailEntity GetWaitApproveDetailByDateRange(string EmpID, string StartDateNo, string EndDateNo)
        {
            // 待批案件筆數
            int WaitApproveDetailItemCount = 0;

            WaitApproveStaticAndDetailEntity WaitApproveStaticAndDetailInfo = new WaitApproveStaticAndDetailEntity();

            // 傳入參數設定與檢查 ==========================================================
            string StartDate = StartDateNo.Substring(0, 4)
                + "/" + StartDateNo.Substring(4, 2)
                + "/" + StartDateNo.Substring(6, 2);

            string EndDate = EndDateNo.Substring(0, 4)
                + "/" + EndDateNo.Substring(4, 2)
                + "/" + EndDateNo.Substring(6, 2);
            if (DateTime.TryParse(StartDate, out DateTime dtStartDate) == false)
            {
                return null;
            }

            if (DateTime.TryParse(EndDate, out DateTime dtEndDate) == false)
            {
                return null;
            }


            // 取得資料 ======================================================================
            string sSQL = "EXEC [whs].[usp_GetWaitApproveDetail] {0}, {1}, {2}";
            var MyHrisDB = new HrisDbContext();
            List<WaitApproveDetailEntity> WaitApproveDetailListInfo = 
                MyHrisDB.WaitApproveDetailEntitys.FromSqlRaw(sSQL, EmpID, dtStartDate, dtEndDate)
                .ToList();

            if (WaitApproveDetailListInfo != null)
                WaitApproveDetailItemCount = WaitApproveDetailListInfo.Count;

            
            WaitApproveStaticAndDetailInfo.WaitApproveStatic = new WaitApproveStaticEntity(WaitApproveDetailItemCount);
            WaitApproveStaticAndDetailInfo.WaitApproveList = WaitApproveDetailListInfo;


            return WaitApproveStaticAndDetailInfo;
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
        ///         "workingDate":"2020-04-01",
        ///         "waitApproveItemCount":0,
        ///         "isFinish":"已批核"
        ///     },
        ///     {
        ///         "workingDate":"2020-04-02",
        ///         "waitApproveItemCount":0,
        ///         "isFinish":"已批核"
        ///     },
        ///     {
        ///         "workingDate":"2020-04-03",
        ///         "waitApproveItemCount":0,
        ///         "isFinish":"已批核"
        ///     },
        ///     {
        ///         "workingDate":"2020-04-06",
        ///         "waitApproveItemCount":10,
        ///         "isFinish":"未批核"
        ///     },
        ///     {
        ///         "workingDate":"2020-04-08",
        ///         "waitApproveItemCount":30,
        ///         "isFinish":"未批核"
        ///     },
        ///     {
        ///         "workingDate":"2020-04-09",
        ///         "waitApproveItemCount":40,
        ///         "isFinish":"未批核"
        ///     },
        ///     {
        ///         "workingDate":"2020-04-10",
        ///         "waitApproveItemCount":20,
        ///         "isFinish":"未批核"
        ///     },
        ///     {
        ///         "workingDate":"2020-04-13",
        ///         "waitApproveItemCount":5,
        ///         "isFinish":"未批核"
        ///     }
        ///     ]
        ///     </h2></pre>
        /// </remarks>
        /// <param name="EmpID" >主管員工代號</param>
        /// <param name="StartDateNo">傳入日期區間：起始日期</param>
        /// <param name="EndDateNo">傳入日期區間：結束日期</param>
        /// <returns>傳回日期區間（通常是單月）內各日的批核總計狀態</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("EmpID/{EmpID}/StartDate/{StartDateNo}/EndDate/{EndDateNo}/GetWaitApproveDayStatic")]
        public List<WaitApproveDayStaticEntity> GetWaitApproveDayStaticByDateRange(string EmpID, string StartDateNo, string EndDateNo)
        {

            // 傳入參數設定與檢查 ==========================================================
            string StartDate = StartDateNo.Substring(0, 4)
                + "/" + StartDateNo.Substring(4, 2)
                + "/" + StartDateNo.Substring(6, 2);

            string EndDate = EndDateNo.Substring(0, 4)
                + "/" + EndDateNo.Substring(4, 2)
                + "/" + EndDateNo.Substring(6, 2);
            if (DateTime.TryParse(StartDate, out DateTime dtStartDate) == false)
            {
                return null;
            }

            if (DateTime.TryParse(EndDate, out DateTime dtEndDate) == false)
            {
                return null;
            }

            // 取得資料 ======================================================================
            string sSQL = "EXEC [whs].[usp_GetWaitApproveDayStatic] {0}, {1}, {2}";
            var MyHrisDB = new HrisDbContext();
            List<WaitApproveDayStaticEntity> WaitApproveDayStaticListInfo = 
                MyHrisDB.WaitApproveDayStaticEntitys
                    .FromSqlRaw(sSQL, EmpID, dtStartDate,dtEndDate)
                    .ToList();


            //string sJson = @"[                {                    ""workingDate"":""2020-04-01"",                    ""waitApproveItemCount"":0,                    ""isFinish"":""已批核""                },                {                    ""workingDate"":""2020-04-02"",                    ""waitApproveItemCount"":0,                    ""isFinish"":""已批核""                },                {                    ""workingDate"":""2020-04-03"",                    ""waitApproveItemCount"":0,                    ""isFinish"":""已批核""                },                {                    ""workingDate"":""2020-04-06"",                    ""waitApproveItemCount"":10,                    ""isFinish"":""未批核""                },                {                    ""workingDate"":""2020-04-08"",                    ""waitApproveItemCount"":30,                    ""isFinish"":""未批核""                },                {                    ""workingDate"":""2020-04-09"",                    ""waitApproveItemCount"":40,                    ""isFinish"":""未批核""                },                {                    ""workingDate"":""2020-04-10"",                    ""waitApproveItemCount"":20,                    ""isFinish"":""未批核""                },                {                    ""workingDate"":""2020-04-13"",                    ""waitApproveItemCount"":5,                    ""isFinish"":""未批核""                }                ]";

            return WaitApproveDayStaticListInfo;
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
        [HttpGet("FlowID/{FlowID}/GetApplicationDetail")]
        public ContentResult GetGetApplicationDetail(string FlowID)
        {

            string sSQL = "EXEC [whs].[usp_GetAlterbyEmpID] {0}";
            var MyHrisDB = new HrisDbContext();

            //List<ProcessStatusEntity> ProcessStatusListInfo = MyHrisDB.TB_ProcessStatusEntitys
            //    .Where(e => e.FlowID == FlowID).ToList();

            //List<ProcessStatusDetailEntity> ProcessStatusDetailListInfo = MyHrisDB.TB_ProcessStatusDetailEntitys
            //    .Where(e => e.FlowID == FlowID).ToList();

            //var ProcessStatusListInfo = MyHrisDB.TB_ProcessStatusEntitys
            //    .Where(e => e.FlowID == FlowID);

            //var sql = SqlContextHelper.ToSql(ProcessStatusListInfo);


            //var ProcessStatusDetailListInfo = MyHrisDB.TB_ProcessStatusDetailEntitys
            //    .Where(e => e.FlowID == FlowID);


            string sJson = @"{  ""home"": {    ""empName"": ""彭政閔""  },  ""workdateList"": [    {      ""rowUnid"": ""00DALSGJ-56D4-4C65-906B-F5F4AA23A4BF"",      ""workingDate"": ""2020-04-06"",      ""workingHourDetailList"": [        {          ""typeCode"": ""B01"",          ""typeName"": ""專標案(建置)"",          ""jobCode"": ""GA090054S"",          ""jobName"": ""國立暨南大學資工系教育行政資訊系統研發中心主機弱掃需求   "",          ""workingHours"": 1,          ""note"": ""TEST1""        },        {          ""typeCode"": ""B01"",          ""typeName"": ""專標案(建置)"",          ""jobCode"": ""DA090138S"",          ""jobName"": ""彰化銀行升速2路NG-SDH 100M數據專線     "",          ""workingHours"": 2,          ""note"": ""TEST2""        }      ]    },    {      ""rowUnid"": ""00DF544D-6D76-4CAA-666B-B0A0B2B3A4BF"",      ""workingDate"": ""2020-04-08"",      ""workingHourDetailList"": [        {          ""typeCode"": ""S01"",          ""typeName"": ""專標案(建置)"",          ""jobCode"": ""GA0SSSSSS"",          ""jobName"": ""國立SSSSSSS政資訊系統研發中心主機弱掃需求   "",          ""workingHours"": 1,          ""note"": ""TESTSSS1""        },        {          ""typeCode"": ""S02"",          ""typeName"": ""專標案(建置)"",          ""jobCode"": ""DA0SSS222S"",          ""jobName"": ""SSS22銀行升速2路NG-SDH 100M數據專線     "",          ""workingHours"": 2,          ""note"": ""TESTS2""        }      ]    },    {      ""rowUnid"": ""00A05C2D-5DS4-4C65-556B-B54S4S5622BF"",      ""workingDate"": ""2020-04-09"",      ""workingHourDetailList"": [        {          ""typeCode"": ""S01"",          ""typeName"": ""專標案(建置)"",          ""jobCode"": ""GA0GGG11"",          ""jobName"": ""國立GGGGG政資訊系統研發中心主機弱掃需求   "",          ""workingHours"": 1,          ""note"": ""TESTGGGG1""        },        {          ""typeCode"": ""S02"",          ""typeName"": ""專標案(建置)"",          ""jobCode"": ""DA0GGG22"",          ""jobName"": ""GGGG22銀行升速2路NG-SDH 100M數據專線     "",          ""workingHours"": 2,          ""note"": ""TESTG2""        }      ]    },    {      ""rowUnid"": ""00D5252D-66D4-4ASD-333B-B0A498S5S4BF"",      ""workingDate"": ""2020-04-10"",      ""workingHourDetailList"": [        {          ""typeCode"": ""S01"",          ""typeName"": ""專標案(建置)"",          ""jobCode"": ""GA0HHH1"",          ""jobName"": ""國立HHHHHH政資訊系統研發中心主機弱掃需求   "",          ""workingHours"": 1,          ""note"": ""TESTHHH1""        },        {          ""typeCode"": ""S02"",          ""typeName"": ""專標案(建置)"",          ""jobCode"": ""DA0HHHH2"",          ""jobName"": ""HHH22銀行升速2路NG-SDH 100M數據專線     "",          ""workingHours"": 2,          ""note"": ""TESTH2""        }      ]    }  ]}";

            return new ContentResult { Content = sJson, ContentType = "application/json" };
        }






       












    }
}