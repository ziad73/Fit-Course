
using BLL.DTOS.QuestionDTOs;
using BLL.Services;
using DAL.Entities.quiz;
using Microsoft.AspNetCore.Mvc;
using PLL.Areas.Admin.ViewsModels.QuestionVM;

namespace PLL.Areas.Admin.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _QS;

        public QuestionController(IQuestionService sS)
        {
            _QS = sS;
        }
        [HttpGet]
        public IActionResult Index(int QuizId)
        {
            HttpContext.Session.SetInt32("CurrentQuizId", QuizId);

            return View("~/Areas/Admin/Views/ManageQuestion/Index.cshtml");
        }
        public IActionResult AddQuestion()
        {
            return PartialView("~/Areas/Admin/Views/ManageQuestion/_AddQuestion.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> EditQuestion(int QuestionId)
        {
            QuestionDTO s = new QuestionDTO();
            s = await _QS.GetById(QuestionId);
            if (s == null)
                return BadRequest("No Quiz selected.");
            return PartialView("~/Areas/Admin/Views/ManageQuestion/_EditQuestion.cshtml", s);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewQuestion(QuestionDTO s)
        {
            var QuizId = HttpContext.Session.GetInt32("CurrentQuizId");
            if (QuizId == null)
                return BadRequest("No Quiz selected.");
            s.QuizId = QuizId.Value;
            if (ModelState.IsValid)
            {
                var result = await _QS.Create(s);
                if (result != null)
                    return Json(new { success = true, message = "Question created successfully" });
                else
                    return Json(new { success = false, message = "Failed to create Question" });
            }
            string allErrorsText = string.Empty;
            var allErrors = ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage)
         .ToList();
            foreach (string error in allErrors)
            {
                allErrorsText += $"{error}, ";
            }
            return Json(new { success = false, message = allErrorsText });

        }
        [HttpPost]
        public async Task<IActionResult> SaveEditQuestion(QuestionDTO s)
        {
            if (ModelState.IsValid)
            {
                var result = await _QS.Update(s);
                if (result != null)
                    return Json(new { success = true, message = "Question updated successfully" });
                else
                    return Json(new { success = false, message = "Failed to udpate Question" });
            }
            string allErrorsText = string.Empty;
            var allErrors = ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage)
         .ToList();
            foreach (string error in allErrors)
            {
                allErrorsText += $"{error}, ";
            }
            return Json(new { success = false, message = allErrorsText });

        }
        [HttpGet]
        public async Task<IActionResult> AllQuestions(int pageSize = 3, int pageNumber = 1)
        {
            var QuizId = HttpContext.Session.GetInt32("CurrentQuizId");
            if (QuizId == null)
                return BadRequest("No Quiz selected.");

            var Questions = await _QS.GetList(q=>q.QuizId==QuizId);

            var pagedQuestions = Questions
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalSections = Questions.Count;

            return PartialView("~/Areas/Admin/Views/ManageQuestion/_AllQuestions.cshtml", new PagedQuestions
            {
                Questions = pagedQuestions,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalSections / (double)pageSize)
            });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (await _QS.Delete(id))
                return Json(new { success = true, message = "The Question is deleted Successfuly." });
            return Json(new { success = true, message = "The Operation is Failed." });

        }
    }
}
