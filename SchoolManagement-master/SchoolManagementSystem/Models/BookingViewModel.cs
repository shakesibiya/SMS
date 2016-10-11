using SchoolManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class BookingViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime BookTime { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public virtual Event BookedEvent { get; set; }
        public virtual Student BookedStudent { get; set; }
    }
}