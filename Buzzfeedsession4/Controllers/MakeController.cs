using Buzzfeedsession4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buzzfeedsession4.Controllers
{
    public class MakeController : Controller
    {
        DbConnection newConnection = new DbConnection();

        // GET: Quiz
        public ActionResult Index()
        {
            List <Quiz> quizzes = newConnection.Quizzes.ToList();
            return View(quizzes);
        }

        //Get: Make/Quiz/:id
        public ActionResult Quiz(int id)
        {
            Quiz currentQuiz = newConnection.Quizzes.Find(id);
            return View(currentQuiz);
        }

        // GET: Quiz
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string title)
        {
            //create a new quiz 
            //save in db
            Quiz newQuiz = new Quiz();
            newQuiz.title = title;
            newConnection.Quizzes.Add(newQuiz);
            newConnection.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Quiz
        [HttpGet]
        public ActionResult NewQuestion(int id)
        {
            ViewBag.quiz_id = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateQuestion(string question, int quiz_id)
        {
            //create a new q 
            //save in db
            Question newQuestion = new Question();
            newQuestion.question = question;
            newQuestion.quiz = newConnection.Quizzes.Find(quiz_id);
            newConnection.Questions.Add(newQuestion);
            newConnection.SaveChanges();
            return RedirectToAction("Quiz", new {
            id = quiz_id});
        }

        public ActionResult newAnswer(int id)
        {
            Question question = newConnection.Questions.Find(id);

            return View(question);
        }

        public ActionResult ConfirmAnswer(string answer, int question_id, int value)
        {
            Answer temp = new Answer();
            temp.answer = answer;
            temp.value = value;
            temp.question = newConnection.Questions.Find(question_id);
            newConnection.Answers.Add(temp);
            newConnection.SaveChanges();
            
            return RedirectToAction("newAnswer", new { id = question_id });
        }
    }
}