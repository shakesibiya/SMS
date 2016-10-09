using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Post
    {
        [Key]
        public long Id { get; set; }

        //public string PIN { get; set; }
        //[ForeignKey("PIN")]
        //public virtual Teacher Teacher { get; set; }

        public string Author { get; set; }

        [Required(ErrorMessage ="A post must have a topic")]
        [MaxLength(50)]
        public string Topic { get; set; }

        [Required(ErrorMessage = "A post needs to have some text.")]
        [StringLength(200, ErrorMessage = "You have exceeded the character count, please remove some words and try again")]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool? Public { get; set; }

        public virtual List<PostPictures> Pictures { get; set; }
    }
}