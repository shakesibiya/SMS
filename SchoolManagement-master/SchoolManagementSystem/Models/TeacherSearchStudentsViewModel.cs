using SchoolManagementSystem.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagementSystem.Models
{
    public class TeacherSearchStudentsViewModel
    {
        public List<Student> StudentsToStudy { get; set; }
        public Dictionary<string, int> ClassNames { get; set; }
    }
}