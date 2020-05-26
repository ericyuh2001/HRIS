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
        /// <response code="400">參數錯誤</response>     
        /// <response code="500">內部錯誤</response>     
        [HttpPost()]
        public IActionResult SignBatchApproval([FromBody] BatchSignApprovalEntity BatchSignApprovalInfo)
        {
            StatusCodeResult scrReturn = null;
            bool bolInValid = false;
            string sSQL = @"EXEC [whs].[usp_UpdatedProcessStatusBySign] 
                            @FlowID = {0}, 
                            @SignID = {1}, 
                            @SingRemark = {2}, 
                            @IsFinish = {3}, 
                            @UpdatedBy = {4}";
            var MyHrisDB = new HrisDbContext();

            string SignID = BatchSignApprovalInfo.SignID.Trim();
            string IsFinish = "1";                              // 簽准
            string SingRemark = "主管批次核准";
            string UpdatedBy = SignID;
            if (SignID.Length != 6)
            {
                scrReturn = BadRequest();
                bolInValid = true;
            }
                


            // =====================================================================================
            if (bolInValid == false
                && BatchSignApprovalInfo.SignBatchDetailList != null
                && BatchSignApprovalInfo.SignBatchDetailList.Count > 0)
            {
                foreach (SignBatchDetailEntity SignBatchDetailInfo in BatchSignApprovalInfo.SignBatchDetailList)
                {
                    string FlowID = SignBatchDetailInfo.FlowID.Trim();
                    if (FlowID != string.Empty)
                    {
                        string[] param = new[] {
                            FlowID,
                            SignID,
                            SingRemark,
                            IsFinish,
                            UpdatedBy
                        };

                        try
                        {
                            MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
                        }
                        catch (Exception ex)
                        {
                            scrReturn = StatusCode(500);
                            bolInValid = true;
                        }
                    }
                }
            }
            else
            {
                scrReturn = BadRequest();
                bolInValid = true;
            }







            if (bolInValid)
            {
                return scrReturn;
            }
            else
            {
                return Ok();
            }
        }













    }
}
