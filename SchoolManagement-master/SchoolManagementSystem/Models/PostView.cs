using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Models
{
    public class PostView
    {
        public Post Post { get; set; }

        public PostPictures Pics { get; set; }

        public Teacher Author { get; set; } 
    }
}