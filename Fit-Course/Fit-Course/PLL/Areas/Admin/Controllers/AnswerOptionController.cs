using BLL.DTOS.AnswerOptionDTOS;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PLL.Areas.Admin.ViewsModels.AnswerOptionVM;

namespace PLL.Areas.Admin.Controllers
{
    public class AnswerOptionController : Controller
    {
        private readonly IAnswerOptionService _AS;

        public AnswerOptionController(IAnswerOptionService sS)
        {
            _AS = sS;
        }
        [HttpGet]
        public IActionResult Index(int SectionId)
        {
            HttpContext.Session.SetInt32("CurrentQuestionId", SectionId);

            return View("~/Areas/Admin/Views/ManageAnswerOption/Index.cshtml");
        }
        public IActionResult AddAnswerOption()
        {
            return PartialView("~/Areas/Admin/Views/ManageAnswerOption/_AddAnswerOption.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> EditAnswerOption(int AnswerOptionId)
        {
            AnswerOptionDTO s = new AnswerOptionDTO();
            s = await _AS.GetById(AnswerOptionId);
            if (s == null)
                return BadRequest("No AnswerOption selected.");
            return PartialView("~/Areas/Admin/Views/ManageAnswerOption/_EditAnswerOption.cshtml", s);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewAnswerOption(AnswerOptionDTO s)
        {
            var QuestionId = HttpContext.Session.GetInt32("CurrentQuestionId");
            if (QuestionId == null)
                return BadRequest("No Question selected.");
            s.QuestionId = QuestionId.Value;
            if (ModelState.IsValid)
            {
                var result = await _AS.Create(s);
                if (result != null)
                    return Json(new { success = true, message = "AnswerOption created successfully" });
                else
                    return Json(new { success = false, message = "Failed to create AnswerOption" });
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
        public async Task<IActionResult> SaveEditAnswerOption(AnswerOptionDTO s)
        {
            if (ModelState.IsValid)
            {
                var result = await _AS.Update(s);
                if (result != null)
                    return Json(new { success = true, message = "AnswerOption updated successfully" });
                else
                    return Json(new { success = false, message = "Failed to udpate AnswerOption" });
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
        public async Task<IActionResult> AllAnswerOptions(int pageSize = 3, int pageNumber = 1)
        {
            var QuestionId = HttpContext.Session.GetInt32("CurrentQuestionId");
            if (QuestionId == null)
                return BadRequest("No Question selected.");

            var AnswerOptions = await _AS.GetList(a=>a.QuestionId==QuestionId);

            var pagedAnswerOptions = AnswerOptions
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalSections = AnswerOptions.Count;

            return PartialView("~/Areas/Admin/Views/ManageAnswerOption/_AllAnswerOptions.cshtml", new PagedAnswerOptions
            {
                AnswerOptions = pagedAnswerOptions,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalSections / (double)pageSize)
            });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteAnswerOption(int id)
        {
            if (await _AS.Delete(id))
                return Json(new { success = true, message = "The AnswerOption is deleted Successfuly." });
            return Json(new { success = true, message = "The Operation is Failed." });

        }
    }
}
