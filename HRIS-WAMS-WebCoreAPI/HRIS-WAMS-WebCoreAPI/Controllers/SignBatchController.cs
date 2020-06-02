using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HRIS_WAMS_WebCoreAPI.Models;
using Microsoft.EntityFrameworkCore;

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
        ///     POST /api/v1/whs/signBatch   
        ///     {   
        ///         "signID":"692197",
        ///         "isApproval":"1",
        ///         "signBatchDetailList":
        ///         [
        ///             {
        ///                 "FlowID":"1020200413001997002",
        ///                 "WorkingHours":40
        ///             },
        ///             {
        ///                 "FlowID":"1020200421001997001",
        ///                 "WorkingHours":40
        ///             }
        ///         ]
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>     
        /// <response code="501">內部錯誤</response>     
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public IActionResult SignBatchApproval([FromBody] BatchSignApprovalEntity BatchSignApprovalInfo)
        {
            string FlowIDArrayString = string.Empty;
            string SignID = BatchSignApprovalInfo.SignID.Trim();
            string SingRemark = "SP主管批次核准";
            string UpdatedBy = SignID;
            if (SignID.Length != 6)
            {
                return StatusCode(401);
            }



            // =====================================================================================
            if (BatchSignApprovalInfo.SignBatchDetailList != null
                && BatchSignApprovalInfo.SignBatchDetailList.Count > 0)
            {
                foreach (SignBatchDetailEntity SignBatchDetailInfo in BatchSignApprovalInfo.SignBatchDetailList)
                {
                    string currentFlowID = SignBatchDetailInfo.FlowID.Trim();
                    if (FlowIDArrayString == string.Empty)
                        FlowIDArrayString = currentFlowID;
                    else
                        FlowIDArrayString = FlowIDArrayString + "," + currentFlowID;

                }
            }
            else
            {
                return StatusCode(401);
            }


            // ==================================================================
            string sSQL = @"EXEC [whs].[usp_UpdatedProcessStatusBySignBatch] 
                            @FlowID = {0}, 
                            @SignID = {1}, 
                            @SingRemark = {2}, 
                            @UpdatedBy = NULL";

            string[] param = new[] {
                            FlowIDArrayString,
                            SignID,
                            SingRemark,
                            UpdatedBy
                        };

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
