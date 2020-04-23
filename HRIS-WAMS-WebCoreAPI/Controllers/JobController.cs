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




        /// <summary>
        /// <returns>傳回取得單日JobCode清單</returns>
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     GET /api/v1/whs/job/846501/20200105
        ///     {
        ///        "typeCode":"C01 ",
        ///        "typeName":"CT/ICT",
        ///        "jobCode":"Bus_56  ",
        ///        "jobName":"企業整合專案"
        ///     }
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