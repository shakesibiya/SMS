using System.Collections.Generic;

namespace SchoolManagementSystem.Models
{
    public class SecretarySearchModel
    {
        public List<TeacherViewModel> Teachers { get; set; }
        public List<StudentViewModel> Students { get; set; } 
    }
}