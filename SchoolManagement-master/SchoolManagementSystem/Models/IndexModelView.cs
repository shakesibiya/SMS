using SchoolManagementSystem.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagementSystem.Models
{
    public class IndexModelView
    {
        public List<Post> Posts { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public List<Event> Events { get; set; }
    }
}