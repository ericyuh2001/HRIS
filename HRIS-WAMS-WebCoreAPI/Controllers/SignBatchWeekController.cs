using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HRIS_WAMS_WebCoreAPI.Models;

namespace HRIS_WAMS_WebCoreAPI.Controllers
{
    [Route("api/v1/whs/[controller]")]
    [ApiController]
    public class SignBatchWeekController : ControllerBase
    {


        /// <summary>
        /// 主管批次核准（單週全部核准）
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 傳入範例   
        ///     POST /api/v1/whs/signBatchWeek 
        ///     {   
        ///         "signID":"692197",
        ///         "isApproval":"1",
        ///         "startDate":"2020-04-05",
        ///         "endDate":"2020-04-10",
        ///         "waitApproveItemCount":100
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <returns></returns>
        [HttpPost()]
        public void SignBatchApprovalByDateRange([FromBody] BatchSignApprovalEntity BatchSignApprovalInfo)
        {

        }

    }
}