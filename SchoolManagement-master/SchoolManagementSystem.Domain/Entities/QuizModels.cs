﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{





    [Table("Quizzes")]
    public class Quiz
    {
        public Quiz()
        {
            this.Questions = new List<Question>();
        }

        [Key]
        public int QuizID { get; set; }
        public string TeacherName { get; set; }
        public string QuizName { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }

    [Table("Questions")]
    public class Question
    {
        public Question()
        {
            this.Answers = new List<Answer>();
        }

        [Key]
        public int QuestionID { get; set; }

        public virtual Answer CorrectAnswer { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }

    [Table("Answers")]
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        public string Text { get; set; }
    }

    [Table("QuizAttempts")]
    public class QuizAttempt
    {
        public QuizAttempt()
        {
            this.Answers = new List<QuizAnswer>();
        }

        [Key]
        public int QuizAttemptID { get; set; }
        public string StudentName { get; set; }

        public int QuizID { get; set; }

        [ForeignKey("QuizID")]
        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<QuizAnswer> Answers { get; set; }
    }

    public class QuizAnswer
    {
        [Key]
        public int QuizAnswerID { get; set; }
        public int? QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }
        public int? AnswerID { get; set; }
        [ForeignKey("AnswerID")]
        public virtual Answer Answer { get; set; }
    }
}