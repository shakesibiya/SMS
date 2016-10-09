using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class AppointmentByStudentModel : AppointmentModel
    {
        [Display(Name = "Teacher")]
        public TeacherViewModel Teacher { get; set; }
    }
}
