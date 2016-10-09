using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class AddGroupView
    {
        public int VotingId { get; set; }

        [Required(ErrorMessage = "You must select a Group")]
        public int GroupId { get; set; }
    }
}
