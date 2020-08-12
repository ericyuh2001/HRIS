using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.CompilerServices;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    [Table("ApiExceptionLog", Schema = "whs")]
    public class ApiExceptionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogSN { get; set; }

        [MaxLength(18)]
        [Required]
        public string ProcedureID { get; set; }

        [MaxLength(6)]
        [AllowNull]
        public string EmpID { get; set; }

        [MaxLength(8)]
        [AllowNull]
        public string DateNo1 { get; set; }

        [MaxLength(8)]
        [AllowNull]
        public string DateNo2 { get; set; }

        [MaxLength(100)]
        [AllowNull]
        public string ProcedureStepDescription { get; set; }

        [AllowNull]
        public int ExceptionHResult { get; set; }

        [MaxLength(100)]
        [AllowNull]
        public string ExceptionMessage { get; set; }


        [AllowNull]
        public DateTime CreatedDate { get; set; }

    }




    public class ApiExceptionLogHelper
    {
        public static async Task<int> AddLogSync(HrisDbContext thisDbContext
            ,Exception thisException, ApiExceptionLog thisAEL) 
        {
            
            try
            {
                thisAEL.ExceptionHResult = thisException.HResult;
                thisAEL.ExceptionMessage = thisException.Message;

                await thisDbContext.TB_ApiExceptionLogs.AddAsync(thisAEL);
                return await thisDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
           

            return 0;
        }
    }


}
