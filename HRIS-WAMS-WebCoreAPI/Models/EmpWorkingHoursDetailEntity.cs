using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class EmpWorkingHoursDetailEntity
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
        public int WorkingHours { get; set; }


        [Required]
        [Display(Name = "備註")]
        public string Note { get; set; }



        [Required]
        [Display(Name = "填報人")]
        public string CreatedBy { get; set; }

    }








    public class EmpWorkingHoursDetailUpdEntity
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
        public int WorkingHours { get; set; }


        [Required]
        [Display(Name = "備註")]
        public string Note { get; set; }


        [Required]
        [Display(Name = "填報人")]
        public string CreatedBy { get; set; }

    }




    public class EmpWorkingHoursDetailDelEntity
    {
        [Required]
        [Display(Name = "工時代碼")]
        public string RowUnid { get; set; }


        [Display(Name = "刪除")]
        public string DeletedBy { get; set; }

    }



}
