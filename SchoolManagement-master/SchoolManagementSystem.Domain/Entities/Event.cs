using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Event
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Event Name")]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(180)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        [MaxLength(60)]
        public string Venue { get; set; }

        [MaxLength(180)]
        public string More { get; set; }

        public bool Archive { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? ArchiveDate { get; set; }
    }

    public class EventComment
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public long EventId { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }
    }
}