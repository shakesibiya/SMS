using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{
    public class room
    {
        [Key]
        public int room_id { get; set; }
        public string roomname { get; set; }


    }
}