using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Domain.Entities
{
    public class PostPictures
    {
        [Key]
        public long Id { get; set; }

        public long PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public string FileName { get; set; }

    }
}