using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS_WAMS_WebCoreAPI.Models;

//using Swashbuckle.AspNetCore.Swagger;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using Swashbuckle.AspNetCore.SwaggerUI;


namespace HRIS_WAMS_WebCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class HrisWamsController : ControllerBase
    {
        private readonly HrisDbContext _context;

        public HrisWamsController()
        {
            
        }

        //public HrisWamsController(HrisDbContext context)
        //{
        //    //InvalidOperationException: Unable to resolve service for type 'HRIS_WAMS_WebCoreAPI.Models.HrisDbContext' while attempting to activate 'HRIS_WAMS_WebCoreAPI.Controllers.HrisWamsController'.
        //    //Microsoft.Extensions.DependencyInjection.ActivatorUtilities.GetService(IServiceProvider sp, Type type, Type requiredBy, bool isDefaultParameterRequired)
        //    _context = context;
        //}

        // GET: api/HrisWams
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<EmpLeaveWorkDateEntity>>> GetEmpLeaveWorkDateEntities()
        //{
        //    return await _context.EmpLeaveWorkDateEntities.ToListAsync();
        //}

        /// <summary>
        /// 測試SP：查詢員工休假......
        /// </summary>
        /// <remarks>
        /// 回傳範例
        ///     GET /GetEmpLeaveWorkDates
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
        [HttpGet("{EmpID}/{WorkDateNo}")]
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
        /// 測試SP：更新員工休假............
        /// </summary>
        /// <param name="EmpID">員工編號</param>
        /// <param name="WorkDateNo">上班日（格式yyyymmmdd）</param>
        /// <returns>目前無資料，純測試</returns>
        /// <remarks>
        /// 傳入範例
        ///     POST /GetEmpLeaveWorkDates
        ///     {
        ///        "EmpID": 123456,
        ///        "WorkDate": "2010/01/02",
        ///        "ApplyHours": 8,
        ///        "Reason":"休假",
        ///        "type":"Leave",
        ///        "IsProject":"1"
        ///     }
        /// </remarks>
        /// <response code="201">代碼201說明描述</response>
        /// <response code="400">代碼401說明描述</response>          
        /// <returns></returns>
        [HttpPost("{EmpID}/{WorkDateNo}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<EmpLeaveWorkDateEntity> UpdateEmpLeaveWorkDates(string EmpID, string WorkDateNo)
        {

            DateTime WorkDate = DateTime.Parse( 
                    WorkDateNo.Substring(0, 4)
                   + "/" + WorkDateNo.Substring(4, 2)
                   + "/" + WorkDateNo.Substring(6, 2)
                   );

            EmpLeaveWorkDateEntity EmpInfo = new EmpLeaveWorkDateEntity()
            {
                EmpID = EmpID,
                WorkDate = WorkDate,
                Reason = "POST傳送"
            };

            List<EmpLeaveWorkDateEntity> lstEmp = new List<EmpLeaveWorkDateEntity>();
            lstEmp.Add(EmpInfo);
           
            return lstEmp;
        }


        // GET: api/HrisWams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpLeaveWorkDateEntity>> GetEmpLeaveWorkDateEntity(string id)
        {
            var empLeaveWorkDateEntity = await _context.EmpLeaveWorkDateEntities.FindAsync(id);

            if (empLeaveWorkDateEntity == null)
            {
                return NotFound();
            }

            return empLeaveWorkDateEntity;
        }

        // PUT: api/HrisWams/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpLeaveWorkDateEntity(string id, EmpLeaveWorkDateEntity empLeaveWorkDateEntity)
        {
            if (id != empLeaveWorkDateEntity.EmpID)
            {
                return BadRequest();
            }

            _context.Entry(empLeaveWorkDateEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpLeaveWorkDateEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HrisWams
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EmpLeaveWorkDateEntity>> PostEmpLeaveWorkDateEntity(EmpLeaveWorkDateEntity empLeaveWorkDateEntity)
        {
            _context.EmpLeaveWorkDateEntities.Add(empLeaveWorkDateEntity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmpLeaveWorkDateEntityExists(empLeaveWorkDateEntity.EmpID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmpLeaveWorkDateEntity", new { id = empLeaveWorkDateEntity.EmpID }, empLeaveWorkDateEntity);
        }

        // DELETE: api/HrisWams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmpLeaveWorkDateEntity>> DeleteEmpLeaveWorkDateEntity(string id)
        {
            var empLeaveWorkDateEntity = await _context.EmpLeaveWorkDateEntities.FindAsync(id);
            if (empLeaveWorkDateEntity == null)
            {
                return NotFound();
            }

            _context.EmpLeaveWorkDateEntities.Remove(empLeaveWorkDateEntity);
            await _context.SaveChangesAsync();

            return empLeaveWorkDateEntity;
        }

        private bool EmpLeaveWorkDateEntityExists(string id)
        {
            return _context.EmpLeaveWorkDateEntities.Any(e => e.EmpID == id);
        }
    }
}
