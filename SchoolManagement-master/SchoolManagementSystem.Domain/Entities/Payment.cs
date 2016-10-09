using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Price")]
        [Required]
        public decimal Value { get; set; }
        
        public string Type { get; set; }

        [DisplayName("Date Paid")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Student Pin")]
        public string Student_PIN { get; set; }

        [Required]
        public virtual int Discipline_Id { get; set; }

        [Required]
        public virtual int StudentID { get; set; }
    }
}