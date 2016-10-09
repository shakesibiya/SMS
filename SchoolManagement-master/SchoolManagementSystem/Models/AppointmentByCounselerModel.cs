using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolManagementSystem.Models
{
    public class AppointmentByCounselerModel : AppointmentModel
    {
        [Required]
        [Display(Name = "Student")]
        public int StudentId { get; set; }

        public IEnumerable<SelectListItem> StudentsList { get; set; }
    }
}
