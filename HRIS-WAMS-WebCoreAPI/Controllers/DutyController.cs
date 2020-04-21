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
    public class DutyController : ControllerBase
    {
        private readonly HrisDbContext _context;

        public DutyController()
        {
        }







        /// <summary>
        /// 刪除填報資料
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     DELETE /api/v1/whs/duty
        ///     {
        ///         "RowUnid":"11111CF1-4449-4772-8601-58A478DF110B",
        ///         "DeletedBy":"001234"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>     
        [HttpDelete]
        public async Task<ActionResult<EmpWorkingHoursDetailDelEntity>> DeleteWorkingHoursDetail([FromBody]EmpWorkingHoursDetailDelEntity empWorkingHoursDetailEntity)
        {
            

            string RowUnid = empWorkingHoursDetailEntity.RowUnid;
            string DeletedBy = empWorkingHoursDetailEntity.DeletedBy;


            string[] param = new[] {
                RowUnid
            };

            string sSQL = @"EXEC [whs].[usp_DeleteWorkingHoursDetail] 
                        @RowUnid = {0}";
            var MyHrisDB = new HrisDbContext();


            try            
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
            }
            catch
            {
                return BadRequest();
            }
            



            return NoContent();


        }




        /// <summary>
        /// 取得員工單日加班、請假等資料
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/EmpID/002688/GetAlterbyEmpID
        ///     {
        ///        "message":"您尚有 2020/04/07 未填寫完成!"
        ///     }
        /// </remarks>
        /// <param name="EmpID" >員工編號</param>
        /// <returns>傳回員工上班日休假資料表</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("EmpID/{EmpID}/GetAlterbyEmpID")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<EmployeeMessageEntity> GetAlterbyEmpID(string EmpID)
        {

            string sSQL = "EXEC [whs].[usp_GetAlterbyEmpID] {0}";
            var MyHrisDB = new HrisDbContext();
            var EmpMessage = MyHrisDB.EmployeeMessageEntitys.FromSqlRaw(sSQL, EmpID);

            return EmpMessage;
        }







        /// <summary>
        /// 取得員工單日加班、請假等資料
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/EmpID/002688/WorkDate/20160223/GetEmpLeavebyWorkDate
        ///     {
        ///        "EmpID": 123456,
        ///        "WorkDate": "2010/01/02",
        ///        "ApplyHours": 8,
        ///        "Reason":"休假",
        ///        "type":"Leave",
        ///        "IsProject":"1"
        ///     }
        /// </remarks>
        /// <param name="EmpID" >員工編號</param>
        /// <param name="WorkDateNo">上班日（格式yyyymmmdd）</param>
        /// <returns>傳回員工上班日休假資料表</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("EmpID/{EmpID}/WorkDate/{WorkDateNo}/GetEmpLeavebyWorkDate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<EmpLeaveWorkDateEntity> GetEmpLeaveWorkDates(string EmpID, string WorkDateNo)
        {
            string WorkDate = WorkDateNo.Substring(0, 4)
                + "/" + WorkDateNo.Substring(4, 2)
                + "/" + WorkDateNo.Substring(6, 2);
            string sSQL = "EXEC [whs].[usp_GetEmpLeavebyWorkDate] {0}, {1}";
            var MyHrisDB = new HrisDbContext();
            var EmpWorkDateList = MyHrisDB.EmpLeaveWorkDateEntities.FromSqlRaw(sSQL, EmpID, WorkDate);

            return EmpWorkDateList;
        }


















        /// <summary>
        /// 抓取員工首頁資訊
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/EmpID/692197/GetHomeInfoByEmp
        ///     {
        ///        "EmpID":"692197",
        ///        "EmpName":"李蕙芬",
        ///        "Sex":"2",
        ///        "message":"您目前尚無待辦事項須處理",
        ///     }
        /// </remarks>
        /// <param name="EmpID" >員工代號</param>
        /// <returns>傳回員工首頁資訊</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("EmpID/{EmpID}/GetHomeInfoByEmp")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<HomeInfoByEmpEntity> GetHomeInfoByEmp(string EmpID)
        {
            string sSQL = "EXEC [whs].[usp_GetHomeInfoByEmp] {0}";
            var MyHrisDB = new HrisDbContext();
            var HomeInfoByEmpList = MyHrisDB.HomeInfoByEmpEntitys.FromSqlRaw(sSQL, EmpID);

            return HomeInfoByEmpList;
        }









        /// <summary>
        /// 員工萬年曆狀態列表
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/EmpID/373409/StartDate/20200301/EndDate/20200331/GetWorkingDate
        ///     {
        ///        "empID":"373409",
        ///        "workingDate":"2020-03-01T00:00:00",
        ///        "isFinish":"9"
        ///     }
        /// </remarks>
        /// <param name="EmpID" >員工編號</param>
        /// <param name="StartDateNo">傳入日期區間：起始日期</param>
        /// <param name="EndDateNo">傳入日期區間：結束日期</param>
        /// <returns>傳回個人月曆資料清單資料表</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("EmpID/{EmpID}/StartDate/{StartDateNo}/EndDate/{EndDateNo}/GetWorkingDate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<EmpWorkingDateEntity> GetWorkingDate(string EmpID, string StartDateNo,string EndDateNo)
        {
            string StartDate = StartDateNo.Substring(0, 4)
                + "/" + StartDateNo.Substring(4, 2)
                + "/" + StartDateNo.Substring(6, 2);

            string EndDate = EndDateNo.Substring(0, 4)
                + "/" + EndDateNo.Substring(4, 2)
                + "/" + EndDateNo.Substring(6, 2);

            string sSQL = "EXEC [whs].[usp_GetWorkingDate] {0}, {1}, {2}";
            var MyHrisDB = new HrisDbContext();
            var EmpLeaveWorkDateList = MyHrisDB.EmpWorkingDateEntitys.FromSqlRaw(sSQL, EmpID, StartDate,EndDate);

            return EmpLeaveWorkDateList;
        }














/// <summary>
        /// 員工單日可填報項目
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/RowUnid/00A05C2D-56D4-4C65-906B-B0A0B2B3A4BF/TypeCode/C01/JobCode/Bus_1/GetWorkingHoursDetail
        ///     {
        ///        "empID":"373409",
        ///        "workingDate":"2020-03-01T00:00:00",
        ///        "isFinish":"9"
        ///     }
        /// </remarks>
        /// <param name="RowUnid" >工時填報資料代碼</param>
        /// <param name="TypeCode">Type代碼</param>
        /// <param name="JobCode">Job代碼</param>
        /// <returns>傳回員工單日可填報項目</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("RowUnid/{RowUnid}/TypeCode/{TypeCode}/JobCode/{JobCode}/GetWorkingHoursDetail")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<WorkingHoursDetailEntity> GetWorkingHoursDetail(string RowUnid, string TypeCode,string JobCode)
        {

            string sSQL = "EXEC [whs].[usp_GetWorkingHoursDetail] {0}, {1}, {2}";
            var MyHrisDB = new HrisDbContext();
            var WorkingHoursDetailList = MyHrisDB.WorkingHoursDetailEntitys.FromSqlRaw(sSQL, RowUnid, TypeCode,JobCode);

            return WorkingHoursDetailList;
        }











        /// <summary>
        /// 員工單日工時紀錄列表
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     GET /api/v1/whs/duty/RowUnid/00A05C2D-56D4-4C65-906B-B0A0B2B3A4BF/GetWorkingDate
        ///     {
        ///        [{"rowUnid":"00A05C2D-56D4-4C65-906B-B0A0B2B3A4BF",
        ///        "empID":"309426",
        ///        "workingDate":"2020-04-07T00:00:00",
        ///        "totalWorkingHours":0.0,
        ///        "filledHours":8.0,
        ///        "isFinish":"0",
        ///        "typeCode":"C01",
        ///        "typeName":"CT/ICT",
        ///        "jobCode":"Bus_1",
        ///        "jobName":"市話語音",
        ///        "workingHours":3.5,
        ///        "note":"TEST"}]
        ///     }
        /// </remarks>
        /// <param name="RowUnid" >工時填報資料代碼</param>
        /// <returns>傳回工時填報資料</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpGet("RowUnid/{RowUnid}/GetWorkingDate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<WorkingDateAllDetailEntity> GetWorkingDateAllDetail(string RowUnid)
        {
            
            string sSQL = "EXEC [whs].[usp_GetWorkingDateAllDetail] {0}";
            var MyHrisDB = new HrisDbContext();
            var WorkingDateAllDetailList = MyHrisDB.WorkingDateAllDetailEntitys.FromSqlRaw(sSQL, RowUnid);

            return WorkingDateAllDetailList;
        }







        /// <summary>
        /// 建立工時填報明細資料
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     POST /api/v1/whs/duty
        ///     {
        ///         "RowUnid":"00000CF1-4449-4772-8601-58A478DF110B",
        ///         "TypeCode":"C01",
        ///         "JobCode":"Bus_1",
        ///         "WorkingHours":1,
        ///         "Note":"Test_001",
        ///         "CreatedBy":"002688"
        ///     }
        /// </remarks>
        /// <returns>傳回建檔結果</returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        [HttpPost]
        public async Task<ActionResult<EmpWorkingHoursDetailEntity>> InsertWorkingHoursDetail([FromBody]EmpWorkingHoursDetailEntity empWorkingHoursDetailEntity)
        {
            
            string RowUnid = empWorkingHoursDetailEntity.RowUnid;
            string TypeCode = empWorkingHoursDetailEntity.TypeCode;
            string JobCode = empWorkingHoursDetailEntity.JobCode;
            int WorkingHours = empWorkingHoursDetailEntity.WorkingHours;
            string Note = empWorkingHoursDetailEntity.Note;
            string CreatedBy = empWorkingHoursDetailEntity.CreatedBy;


            string[] param = new[] {
                RowUnid,  
                TypeCode, 
                JobCode, 
                WorkingHours.ToString(), 
                Note, 
                CreatedBy
            };

            string sSQL = @"EXEC [whs].[usp_InsertWorkingHoursDetail] 
                        @RowUnid = {0}, 
                        @TypeCode = {1}, 
                        @JobCode = {2}, 
                        @WorkingHours = {3}, 
                        @Note = {4}, 
                        @CreatedBy = {5}";
            var MyHrisDB = new HrisDbContext();

            try
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
            }
            catch
            {
                return BadRequest();
            }
            


            return NoContent();


            //_context.EmpWorkingHoursDetailEntity.Add(empWorkingHoursDetailEntity);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (EmpWorkingHoursDetailEntityExists(empWorkingHoursDetailEntity.RowUnid))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return CreatedAtAction("GetEmpWorkingHoursDetailEntity", new { id = empWorkingHoursDetailEntity.RowUnid }, empWorkingHoursDetailEntity);
        }

















        /// <summary>
        /// 修改填報資料
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     PUT /api/v1/whs/duty
        ///     {
        ///         "RowUnid":"00000CF1-4449-4772-8601-58A478DF110B",
        ///         "TypeCode":"C01",
        ///         "JobCode":"Bus_1",
        ///         "WorkingHours":1,
        ///         "Note":"Test_001",
        ///         "CreatedBy":"002688"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>     
        [HttpPut]
        public async Task<ActionResult<EmpWorkingHoursDetailUpdEntity>> UpdateWorkingHoursDetail([FromBody]EmpWorkingHoursDetailUpdEntity empWorkingHoursDetailEntity)
        {

            string RowUnid = empWorkingHoursDetailEntity.RowUnid;
            string TypeCode = empWorkingHoursDetailEntity.TypeCode;
            string JobCode = empWorkingHoursDetailEntity.JobCode;
            int WorkingHours = empWorkingHoursDetailEntity.WorkingHours;
            string Note = empWorkingHoursDetailEntity.Note;
            string CreatedBy = empWorkingHoursDetailEntity.CreatedBy;


            string[] param = new[] {
                RowUnid,
                TypeCode,
                JobCode,
                WorkingHours.ToString(),
                Note,
                CreatedBy
            };

            string sSQL = @"EXEC [whs].[usp_UpdateWorkingHoursDetail] 
                        @RowUnid = {0}, 
                        @TypeCode = {1}, 
                        @JobCode = {2}, 
                        @WorkingHours = {3}, 
                        @Note = {4}, 
                        @CreatedBy = {5}";
            var MyHrisDB = new HrisDbContext();

            try
            {
                MyHrisDB.Database.ExecuteSqlRaw(sSQL, param);
            }
            catch
            {
                return BadRequest();
            }



            return NoContent();

        }





































    }
}
