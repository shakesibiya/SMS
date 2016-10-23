
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShaleCo.OnlineQuiz.Web.Controllers
{
    
    public class QuizController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();
        //private UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public ActionResult Index()
        {
           
            var quizzes = db.Quizzes.ToList();

            var model = new QuizViewModel.ListViewModel();

            foreach(var quiz in quizzes)
            {
                model.Quizzes.Add(this.MapQuiz(quiz));
            }

            return View(model);
        }

        public ActionResult Attempt(int id)
        {
            var attempt = db.QuizAttempts.FirstOrDefault(e => e.StudentName == User.Identity.Name && e.QuizID == id);

            if(attempt != null)
            {
                return RedirectToAction("Results", new { id = id });
            }

            var quiz = db.Quizzes.First(e => e.QuizID == id);

            return View(quiz);
        }

        [HttpPost]
        public ActionResult Attempt(QuizViewModel.QuizAnswers data)
        {
            var quiz = db.Quizzes.First(e => e.QuizID == data.QuizID);
            var quizAttempt = new QuizAttempt();
            quizAttempt.QuizID = quiz.QuizID;
            quizAttempt.StudentName = User.Identity.Name;
            
            foreach(var answer in data.Answers)
            {
                var quizAnswer = new QuizAnswer();
                quizAnswer.QuestionID = answer.QuestionID;
                quizAnswer.AnswerID = answer.AnswerID;

                quizAttempt.Answers.Add(quizAnswer);
            }

            db.QuizAttempts.Add(quizAttempt);
            db.SaveChanges();

            return null;
        }

        public ActionResult Results(int id)
        {
            var quizAttempt = db.QuizAttempts.FirstOrDefault(e => e.QuizID == id && e.StudentName == User.Identity.Name);

            var resultsViewModel = new QuizViewModel.QuizResults();
            resultsViewModel.Score = this.Score(quizAttempt);
            resultsViewModel.QuizAttempt = quizAttempt;

            return View(resultsViewModel);
        }

        public ActionResult Details(int id)
        {
            var model = new QuizViewModel.QuizDetails();
            model.Quiz = this.MapQuiz(db.Quizzes.FirstOrDefault(e => e.QuizID == id));

            var quizAttempts = db.QuizAttempts.Where(e => e.QuizID == id).ToList();

            var total = 0;
            foreach(var attempt in quizAttempts)
            {
                var score = this.Score(attempt);
                model.Scores.Add(new QuizViewModel.QuizScore(attempt.StudentName, score));
                total += score;
            }

            model.Average = total / (double) model.Scores.Count;

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuizViewModel.Quiz data)
        {
            var quiz = this.MapQuiz(data);
            db.Quizzes.Add(quiz);
            db.SaveChanges();

            return null;
        }

        private Quiz MapQuiz(QuizViewModel.Quiz data)
        {
            var quiz = new Quiz();
            quiz.QuizName = data.QuizName;
            //quiz.TeacherName = User.Identity.Name;

            foreach(var questionData in data.Questions)
            {
                var question = new Question();
                question.Text = questionData.Text;
                question.CorrectAnswer = new Answer() { Text = questionData.CorrectAnswer };

                foreach(var incorrectData in questionData.IncorrectAnswers)
                {
                    question.Answers.Add(new Answer() { Text = incorrectData });
                }

                quiz.Questions.Add(question);
            }

            return quiz;
        }

        private QuizViewModel.Quiz MapQuiz(Quiz data)
        {
            var quiz = new QuizViewModel.Quiz();
            quiz.QuizName = data.QuizName;
            quiz.QuizID = data.QuizID;
            
            foreach(var questionData in data.Questions)
            {
                var question = new QuizViewModel.Question();
                question.Text = questionData.Text;
                question.CorrectAnswer = questionData.CorrectAnswer.Text;

                foreach(var incorrectData in questionData.Answers)
                {
                    question.IncorrectAnswers.Add(incorrectData.Text);
                }

                quiz.Questions.Add(question);
            }

            return quiz;
        }

        private QuizViewModel.QuizAnswers MapQuizAttempt(QuizAttempt data)
        {
            var quizAnswers = new QuizViewModel.QuizAnswers();
            quizAnswers.QuizID = data.QuizID;
            
            foreach(var answer in data.Answers)
            {
                var questionAnswer = new QuizViewModel.QuestionAnswer();
                questionAnswer.AnswerID = (int) answer.AnswerID;
                questionAnswer.QuestionID = (int) answer.QuestionID;
                quizAnswers.Answers.Add(questionAnswer);
            }

            return quizAnswers;
        }

        private int Score(QuizAttempt quizAttempt)
        {
            var correct = 0;

            foreach(var answer in quizAttempt.Answers)
            {
                if(quizAttempt.Quiz.Questions.First(e => e.QuestionID == answer.QuestionID).CorrectAnswer.AnswerID == answer.AnswerID)
                {
                    correct++;
                }
            }

            return correct;
        }
	}
}