using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{
    public class timetable
    {
        [Key]
        public int timetable_id { get; set; }

        public int compus_id { get; set; }
        public virtual Compus Compus { get; set; }
    }
}