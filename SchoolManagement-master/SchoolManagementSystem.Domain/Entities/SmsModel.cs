using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{
    public class SmsModel
    {
        public long Id { get; set; }

        public string SenderId { get; set; }
        public string RecepientId { get; set; }

        [MaxLength(120)]
        public string Text { get; set; }

        public DateTime Date { get; set; }

    }
}