using System;
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
    public class JobController : ControllerBase
    {
        public JobController()
        {
        }




        ///// <summary>
        ///// <returns>傳回取得單日JobCode清單</returns>
        ///// </summary>
        ///// <remarks>
        ///// <pre><h2>
        ///// 回傳範例
        /////     GET /api/v1/whs/job/846501/20200105
        /////     {
        /////        "typeCode":"C01 ",
        /////        "typeName":"CT/ICT",
        /////        "jobCode":"Bus_56  ",
        /////        "jobName":"企業整合專案"
        /////     }
        /////     </h2></pre>
        ///// </remarks>
        ///// <param name="EmpID" >員工代號</param>
        ///// <param name="WorkingDateNo">上班日（格式yyyymmmdd）</param>
        ///// <returns>傳回單日JobCode清單</returns>
        ///// <response code="201">代碼201說明描述</response>
        ///// <response code="400">代碼401說明描述</response>          
        //[HttpGet("{EmpID}/{WorkingDateNo}/GetWorkingDateJobCodebyEmpID")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IEnumerable<WorkingDateJobCodebyEmpIDEndity> GetWorkingDateJobCodebyEmpID(string EmpID, string WorkingDateNo)
        //{
        //    string WorkDate = WorkingDateNo.Substring(0, 4)
        //        + "/" + WorkingDateNo.Substring(4, 2)
        //        + "/" + WorkingDateNo.Substring(6, 2);
        //    string sSQL = "EXEC [whs].[usp_GetWorkingDateJobCodebyEmpID] {0}, {1}";
        //    var MyHrisDB = new HrisDbContext();
        //    var WorkingDateJobCodebyEmpIDList = MyHrisDB.WorkingDateJobCodebyEmpIDEnditys.FromSqlRaw(sSQL, EmpID, WorkDate);

        //    return WorkingDateJobCodebyEmpIDList;
        //}






        /// <summary>
        /// <returns>傳回取得單日JobCode（樹狀）清單</returns>
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/job/846501/20200105
        ///		[
        ///		    {
        ///		    "typeCode": "B01",
        ///		    "typeName": "專標案(備標)",
        ///		    "detailList":
        ///		    [
        ///		        {
        ///     		    "jobCode": "B010000001",
        ///     		    "jobName": "XXXX專標案"
        ///     		},
        ///     		{
        ///     		    "jobCode": "B010000002",
        ///     		    "jobName": "OOO專標案"
        ///     		},
        ///     		{
        ///     		    "jobCode": "B010000003",
        ///     		    "jobName": "WWW專標案"
        ///     		},
        ///     		{
        ///     		    "jobCode": "GA090054S",
        ///     		    "jobName": "國立暨南大學資工系教育行政資訊系統研發中心主機弱掃需求"
        ///     		},
        ///     		{
        ///     		    "jobCode": "GA090055S",
        ///     		    "jobName": "ibon客製化功能讀卡機(含線材)1"
        ///     		}	
        ///     	]
        ///		  },
        ///		  {
        ///		    "typeCode": "B02",
        ///		    "typeName": "專標案(建置)",
        ///		    "detailList":
        ///		    [
        ///		        {
        ///		            "jobCode": "BA090023H",
        ///     		    "jobName": "109年度及110年度差勤管理資訊系統委外維護服務案"
        ///		        },
        ///		        {
        ///		            "jobCode": "BA090041H",
        ///     		    "jobName": "109年度北區交通控制中心電腦硬體子系統維護工作"
        ///		        },
        ///		        {
        ///		            "jobCode": "BA090046H",
        ///     		    "jobName": "(前瞻基礎建設計畫)基隆市政府虛擬主機整合平台建置案後續擴充"
        ///		        }
        ///         ]
        ///		  },
        ///		  {
        ///		    "typeCode": "C01",
        ///		    "typeName": "CT/ICT",
        ///		    "detailList":
        ///		    [
        ///         ]
        ///		  },
        ///		  {
        ///		    "typeCode": "J04",
        ///		    "typeName": "其它",
        ///		    "detailList":
        ///		    [
        ///		        {
        ///	          	    "jobCode": "J04_99_1_2",
        ///	          	    "jobName": "行政助理"
        ///	          	},
        ///	          	{
        ///	          	    "jobCode": "J04_99_1_3",
        ///	          	    "jobName": "內控作業"
        ///	          	},
        ///	          	{
        ///	          	    "jobCode": "J04_99_1_4",
        ///	          	    "jobName": "個資、資安作業"
        ///	          	},
        ///	          	{
        ///	          	    "jobCode": "J04_99_1_5",
        ///	          	    "jobName": "其他行政作業"
        ///	          	},
        ///	          	{
        ///	          	    "jobCode": "J04_99_1_6",
        ///	          	    "jobName": "工會、福委會業務"
        ///	          	}
        ///         ]
        ///     }
        ///     ]
        ///     </h2></pre>
        /// </remarks>
        /// <param name="EmpID" >員工代號</param>
        /// <param name="WorkingDateNo">上班日（格式yyyymmmdd）</param>
        /// <returns>傳回單日JobCode清單</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("{EmpID}/{WorkingDateNo}/GetWorkingDateJobCodebyEmpID")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<WorkingDateJobCodebyEmpIDEndity> GetWorkingDateJobCodebyEmpID(string EmpID, string WorkingDateNo)
        {
            string WorkDate = WorkingDateNo.Substring(0, 4)
                + "/" + WorkingDateNo.Substring(4, 2)
                + "/" + WorkingDateNo.Substring(6, 2);
            string sSQL = "EXEC [whs].[usp_GetWorkingDateJobCodebyEmpID] {0}, {1}";
            var MyHrisDB = new HrisDbContext();
            var WorkingDateJobCodebyEmpIDList = MyHrisDB.WorkingDateJobCodebyEmpIDEnditys.FromSqlRaw(sSQL, EmpID, WorkDate);

            return WorkingDateJobCodebyEmpIDList;
        }






    }
}