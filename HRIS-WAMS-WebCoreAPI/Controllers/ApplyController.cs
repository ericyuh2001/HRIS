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
    public class ApplyController : ControllerBase
    {
        public ApplyController()
        {
        }




        /// <summary>
        /// 申請人將填報資料送批核
        /// </summary>
        /// <remarks>
        /// <pre><h2>
        /// 回傳範例
        ///     POST /api/v1/whs/apply
        ///     {
        ///         "ApplyID":"001997",
        ///         "WorkingDateStart":"2020-03-24",
        ///         "WorkingDateEnd":"2020-03-27"
        ///     }
        /// </h2></pre>
        /// </remarks>
        /// <returns>傳回</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpPost]
        public async Task<ActionResult<ProcessStatusEntityText>> InsertProcessStatusByWorkingDateRange([FromBody]ProcessStatusEntityText ProcessStatusInfo)
        {

            string ApplyID = ProcessStatusInfo.ApplyID;
            string WorkingDateStartText = ProcessStatusInfo.WorkingDateStart;
            string WorkingDateEndText = ProcessStatusInfo.WorkingDateEnd;
           


            string[] param = new[] {
                ApplyID,
                WorkingDateStartText,
                WorkingDateEndText
            };

            string sSQL = @"DECLARE @FlowID CHAR(36)
                        EXEC [whs].[usp_InsertProcessStatusByWorkingDateRange] 
                        @ApplyID = {0}, 
                        @WorkingDateStart = {1}, 
                        @WorkingDateEnd = {2},
                        @CreatedBy = NULL,
                        @CreatedDate = NULL,
                        @FlowID = @FlowID OUTPUT
                        ";
            var MyHrisDB = new HrisDbContext();

            try
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }



            return NoContent();

        }









    }
}