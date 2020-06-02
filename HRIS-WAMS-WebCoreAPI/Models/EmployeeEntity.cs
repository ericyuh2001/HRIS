using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    [Table("Employee", Schema ="common")]
    public class EmployeeEntity
    {
        [Key]
        public string EmpID { get; set; }

        public string EmpName { get; set; }

    }
}
