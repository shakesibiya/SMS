using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Compus
    {
        [Key]
        public int compus_id { get; set; }
        public string compusnam { get; set; }
    }
}