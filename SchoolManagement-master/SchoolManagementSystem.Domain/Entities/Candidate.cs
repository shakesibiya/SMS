using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Candidate
    {
       [Key]
        public int CandidateId { get; set; }
        public int VotingId { get; set; }
        public int StudentID { get; set; }

        public int QuantityVotes { get; set; }

        public virtual Voting Voting { get; set; }

        public virtual Student User { get; set; }

        public virtual ICollection<votingDetail> votingDetails { get; set; }
    }
}