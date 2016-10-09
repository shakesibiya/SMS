using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Teacher
    {
        [Key]
        public string PIN { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string EducationalGrade { get; set; }
        public string Email { get; set; }

        [DataType(DataType.ImageUrl)]
        [StringLength(200, ErrorMessage = "The field {0} can contain maximun {1} and  minimun {2} character", MinimumLength = 5)]
        public string Photo { get; set; }

        public virtual int Discipline_Id { get; set; }

        public virtual List<Class> ClassesToStudy { get; set; }
    }
}
