using System.Collections.Generic;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Models
{
    public class SecretaryAddTeacherViewModel
    {
        public List<Discipline> Disciplines { get; set; }
        public List<Class> Classes { get; set; } 
    }
}