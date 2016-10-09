using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Avatar
    {
        [Key]
        public long Id { get; set; }

        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        public string FileName { get; set; }
    }
}
