using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class AddCandidateView
    {
        public int VotingId { get; set; }

        [Required(ErrorMessage = "You must select an user.....")]
        public int UserId { get; set; }
    }
}