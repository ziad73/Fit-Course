using BLL.DTOS.QuizDTOS;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PLL.Areas.Admin.ViewsModels.QuizVM;

namespace PLL.Areas.Admin.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizService _QS;

        public QuizController(IQuizService sS)
        {
            _QS = sS;
        }
        [HttpGet]
        public IActionResult Index(int SectionId)
        {
            HttpContext.Session.SetInt32("CurrentSectionId", SectionId);

            return View("~/Areas/Admin/Views/ManageQuiz/Index.cshtml");
        }
        public IActionResult AddQuiz()
        {
            return PartialView("~/Areas/Admin/Views/ManageQuiz/_AddQuiz.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> EditQuiz(int QuizId)
        {
            QuizDTO s = new QuizDTO();
            s = await _QS.GetById(QuizId);
            if (s == null)
                return BadRequest("No Quiz selected.");
            return PartialView("~/Areas/Admin/Views/ManageQuiz/_EditQuiz.cshtml", s);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewQuiz(QuizDTO s)
        {
            var SectionId = HttpContext.Session.GetInt32("CurrentSectionId");
            if (SectionId == null)
                return BadRequest("No Section selected.");
            s.SectionId = SectionId.Value;
            if (ModelState.IsValid)
            {
                var result = await _QS.Create(s);
                if (result != null)
                    return Json(new { success = true, message = "Quiz created successfully" });
                else
                    return Json(new { success = false, message = "Failed to create Quiz" });
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
        public async Task<IActionResult> SaveEditQuiz(QuizDTO s)
        {
            if (ModelState.IsValid)
            {
                var result = await _QS.Update(s);
                if (result != null)
                    return Json(new { success = true, message = "Quiz updated successfully" });
                else
                    return Json(new { success = false, message = "Failed to udpate Quiz" });
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
        public async Task<IActionResult> AllQuizs(int pageSize = 3, int pageNumber = 1)
        {
            var SectionId = HttpContext.Session.GetInt32("CurrentSectionId");
            if (SectionId == null)
                return BadRequest("No Section selected.");

            var Quizs = await _QS.GetList(q=>q.SectionId==SectionId);

            var pagedQuizs = Quizs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalSections = Quizs.Count;

            return PartialView("~/Areas/Admin/Views/ManageQuiz/_AllQuizs.cshtml", new PagedQuizes
            {
                Quizs = pagedQuizs,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalSections / (double)pageSize)
            });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            if (await _QS.Delete(id))
                return Json(new { success = true, message = "The Quiz is deleted Successfuly." });
            return Json(new { success = true, message = "The Operation is Failed." });

        }
    }
}
