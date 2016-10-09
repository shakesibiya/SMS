using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Models
{
    public class StudentViewModel
    {
        public Avatar Avats { get; set; }
        public Student Student { get; set; }
        public string ClassName { get; set; }
    }
}