using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Domain.Entities
{
    public class votingDetail
    {
        [Key]
        public int votingDetailId { get; set; }
        public DateTime DateTime { get; set; }               
        public int UserId { get; set; }
        public int CandidateId { get; set; }
        public int VotingID { get; set; }
        //[ForeignKey("VotingID")]
        public virtual Voting Voting { get; set; }
        public virtual Student User { get; set; }
        public virtual Candidate Candidate { get; set; }

    }
}