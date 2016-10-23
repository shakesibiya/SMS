using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Day
    {
        [Key]
        public int day_id { get; set; }
        public string days { get; set; }

        public int subject_id { get; set; }
        public virtual Subject Subject{ get; set; }

        public int room_id { get; set; }
        public virtual room rooms { get; set; }

        public int compus_id { get; set; }
        public virtual Compus Copmus { get; set; }

        public string time { get; set; }
    }
}
