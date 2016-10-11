using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Booking
    {
        [Key]
        public int Bookingid { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime BookTime { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        //foreign Key
        public virtual Event BookedEvent { get; set; }
        public virtual Student BookedStudent { get; set; }
    }
}