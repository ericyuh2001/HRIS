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
        ///        "message":"3月 W4"
        ///        },
        ///        {"EmpName":"賴x芬",
        ///        "message":"4月 W3"
        ///        }
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <param name="EmpID" >員工編號</param>
        /// <returns>傳回簽核待批訊息</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("EmpID/{EmpID}/GetWaitApprove_WHS")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        ///         "FlowID":"1020200413001997001                 ",
        ///         "SignID":"692197",
        ///         "IsApproval":"1"
        ///         "Note":"核准測試001",
        ///         "SignLogDetailInfo":[
        ///             {
        ///             "RowUnid":"",
        ///             "IsReject":""
        ///             }]
        ///     }
        ///     
        /// 傳入範例二（退件）
        ///     POST /api/v1/whs/sign
        ///     {
        ///         "FlowID":"1020200413001997001                 ",
        ///         "SignID":"692197",
        ///         "IsApproval":"0"
        ///         "Note":"退件測試001",
        ///         "SignLogDetailInfo":[
        ///             {
        ///             "RowUnid":"0000FE19-3FBF-4BDC-A535-1EBFD2BA0C16",
        ///             "IsReject":"1"
        ///             },
        ///             {
        ///             "RowUnid":"0003BF09-ECC6-4F18-BF97-5CF76E3468E2-3FBF-4BDC-A535-1EBFD2BA0C16",
        ///             "IsReject":"1"
        ///             }
        ///             ]
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public void SignByFlowID([FromBody]SingleSignActionBody SingleSignActionInfo)
        {
            
            string IsApproval = SingleSignActionInfo.IsApproval;

            
        }








    }
}