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
        /// 回傳範例
        ///     GET /api/v1/whs/sign/EmpID/002688/GetWaitApprove_WHS
        ///     {
        ///        "EmpName":
        ///     }
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


    }
}