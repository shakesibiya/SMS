using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{
    public class SMS
    {
        public long Id { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}