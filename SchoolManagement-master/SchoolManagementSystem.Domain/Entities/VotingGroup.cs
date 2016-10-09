using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Domain.Entities
{
    public class VotingGroup
   {
        [Key]
        public int VotingGroupId { get; set; }

        [Required(ErrorMessage = "You must select a Group")]
        public int VotingId { get; set; }
        [Required(ErrorMessage = "You must select a Group")]
        public int GroupId { get; set; }

        public virtual Voting Voting { get; set; }
        public virtual Group Group { get; set; }
    }
}
