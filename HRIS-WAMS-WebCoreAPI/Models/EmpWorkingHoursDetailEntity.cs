using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HRIS_WAMS_WebCoreAPI.Models
{




    public class InsertWorkingHoursDetailEntity
    {
        [Required]
        [Display(Name = "工時代碼")]
        public string RowUnid { get; set; }

        [Required]
        [Display(Name = "工班類型")]
        public string TypeCode { get; set; }

        [Required]
        [Display(Name = "工班代碼")]
        public string JobCode { get; set; }

        [Required]
        [Display(Name = "填報時數")]
        public decimal WorkingHours { get; set; }


        [AllowNull]
        [Display(Name = "備註")]
        public string Note { get; set; }



        [Required]
        [Display(Name = "填報人")]
        public string CreatedBy { get; set; }

    }







    public class UpdateWorkingHoursDetailEntity
    {
        [Required]
        [Display(Name = "工時代碼")]
        public string RowUnid { get; set; }

        [Required]
        [Display(Name = "工班類型")]
        public string TypeCode { get; set; }

        [Required]
        [Display(Name = "工班代碼")]
        public string JobCode { get; set; }

        [Required]
        [Display(Name = "填報時數")]
        public decimal WorkingHours { get; set; }


        [AllowNull]
        [Display(Name = "備註")]
        public string Note { get; set; }


        [Required]
        [Display(Name = "填報人")]
        public string CreatedBy { get; set; }

    }




  


        public class DeleteWorkingHoursDetailHandleEntity
    {
        [Required]
        [Display(Name = "工時代碼")]
        public string RowUnid { get; set; }


        [Display(Name = "Type代碼")]
        public string TypeCode { get; set; }

        [Display(Name = "Job代碼")]
        public string JobCode { get; set; }

    }



}
