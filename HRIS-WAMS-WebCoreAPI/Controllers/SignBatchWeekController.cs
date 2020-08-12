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
    public class SignBatchWeekController : ControllerBase
    {


        /// <summary>
        /// 主管批次核准（單週全部核准）
        /// </summary>
        /// <remarks>
        /// 傳入範例  
        /// 
        ///     POST /api/v1/whs/signBatchWeek 
        ///     {   
        ///         "signID":"029935",
        ///         "isApproval":"1",
        ///         "startDate":"2020-05-03",
        ///         "endDate":"2020-05-09",
        ///         "waitApproveItemCount":1
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">操作完成</response>
        /// <response code="401">參數錯誤</response>
        /// <response code="501">內部錯誤</response>     
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(501)]
        public IActionResult SignBatchApprovalByDateRange([FromBody] BatchSignApprovalByDateRangeEntity BatchSignApprovalByDateRangeInfo)
        {
            bool bolInValid = false;

            string SignID = BatchSignApprovalByDateRangeInfo.SignID.Trim();
            string IsApproval = BatchSignApprovalByDateRangeInfo.IsApproval.Trim();

            if (DateTime.TryParse(BatchSignApprovalByDateRangeInfo.StartDate, out DateTime dtStartDate) == false)
                bolInValid = true;

            if (DateTime.TryParse(BatchSignApprovalByDateRangeInfo.EndDate, out DateTime dtEndDate) == false)
                bolInValid = true;

            int WaitApproveItemCount = BatchSignApprovalByDateRangeInfo.WaitApproveItemCount;


            if (bolInValid == true || IsApproval != "1")
                return StatusCode(401);

            // =====================================================
            string[] param = new[] {
                SignID,
                dtStartDate.ToString("yyyy/MM/dd"),
                dtEndDate.ToString("yyyy/MM/dd"),
                WaitApproveItemCount.ToString()
            };

            string sSQL = @"EXEC [whs].[usp_UpdatedProcessStatusBySignBatchWeek] 
                        @SignID = {0}, 
                        @startDate = {1}, 
                        @endDate = {2}, 
                        @SingRemark = '單週批核簽准', 
                        @UpdatedBy = NULL";
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