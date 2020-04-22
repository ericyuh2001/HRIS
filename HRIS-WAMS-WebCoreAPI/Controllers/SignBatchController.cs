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
    public class SignBatchController : ControllerBase
    {


        /// <summary>
        /// 主管批次核准
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 傳入範例   
        ///     POST /api/v1/whs/signbatch   
        ///     {   
        ///         "SignID":"692197",
        ///         "IsApproval":"1",
        ///         "ApprovedItemCount":2,
        ///         "SignBatchDetailInfo":[
        ///             {
        ///             "FlowID":"1020200413001997002",
        ///             "WorkingHours":40
        ///             },
        ///             {
        ///             "FlowID":"1020200421001997001",
        ///             "WorkingHours":40
        ///             }
        ///             ]
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <returns></returns>
        [HttpPut()]
        public void SignBatchApproval([FromBody] BatchSignApprovalEntity BatchSignApprovalInfo)
        {

        }

        
    }
}
