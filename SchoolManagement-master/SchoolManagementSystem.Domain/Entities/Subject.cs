using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Subject
    {
        [Key]
        public int subject_id { get; set; }
        public string subjects { get; set; }

    }
}