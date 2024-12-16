using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using QnA.BusinessLogic;
using QnA.Models;
using AJFIlfov.BusinessLogic.Implementation.User;

namespace QnAWebsite.Controllers
{
    public class QnAController : Controller
    {
        private readonly QuestionService _questionService;
        private readonly UsersService _userService;

        public QnAController(QuestionService questionService, UsersService userService)
        {
            _questionService = questionService;
            _userService = userService;
        }

        public IActionResult Index(string searchText = "")
        {
            var questions = _questionService.GetAllQuestions();

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                questions = questions.Where(q => q.Title.ToLower().Contains(searchText) || q.Content.ToLower().Contains(searchText)).ToList();
            }

            return View("Index", questions);
        }

        public IActionResult Details(int id)
        {
            var question = _questionService.GetQuestionById(id);
            if (question == null) return NotFound();

            return View("Details", question);
        }

        public IActionResult AddQuestion()
        {
            return View("AddQuestion");
        }

        [HttpPost]
        public IActionResult AddQuestion(string title, string content)
        {
            _questionService.AddQuestion(title, content);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAnswer(int questionId, string content)
        {
            _questionService.AddAnswer(questionId, content);
            _userService.AddPoints(10); // Points for adding an answer

            return RedirectToAction("Details", new { id = questionId });
        }

        [HttpPost]
        public IActionResult SuggestEdit(int questionId, string suggestedContent)
        {
            _questionService.AddSuggestion(questionId, suggestedContent);
            _userService.AddPoints(5); // Points for suggesting an edit

            return RedirectToAction("Details", new { id = questionId });
        }

        [HttpPost]
        public IActionResult MarkBestAnswer(int questionId, int answerId)
        {
            _questionService.MarkBestAnswer(questionId, answerId);

            var answerAuthorId = _questionService.GetAnswerAuthorId(answerId);
            _userService.AddPoints(answerAuthorId, 20); // Points for best answer

            return RedirectToAction("Details", new { id = questionId });
        }

        [HttpPost]
        public IActionResult UpvoteAnswer(int answerId)
        {
            _questionService.UpvoteAnswer(answerId);
            return RedirectToAction("Details", new { id = _questionService.GetQuestionIdByAnswerId(answerId) });
        }

        [HttpPost]
        public IActionResult DownvoteAnswer(int answerId)
        {
            _questionService.DownvoteAnswer(answerId);
            return RedirectToAction("Details", new { id = _questionService.GetQuestionIdByAnswerId(answerId) });
        }
    }
}
