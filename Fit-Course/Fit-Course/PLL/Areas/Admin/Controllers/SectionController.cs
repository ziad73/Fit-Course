using BLL.DTOS.CourseDTOS;
using BLL.DTOS.SectionDTOS;
using BLL.Services;
using DAL.Entities.course;
using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionService _SS;

        public SectionController(ISectionService sS)
        {
            _SS = sS;
        }
        [HttpGet]
        public  IActionResult Index(int courseId)
        {
            HttpContext.Session.SetInt32("CurrentCourseId", courseId);

            return View("Index");
        }
        public IActionResult AddSection()
        {
            return PartialView("~/Views/AdminDashboard/ManageSection/_AddSection.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> EditSection(int sectionId)
        {
            SectionDTO s = new SectionDTO();
            s = await _SS.GetById(sectionId);
            if (s == null)
                return BadRequest("No section selected.");
            return PartialView("~/Views/AdminDashboard/ManageSection/_EditSection.cshtml", s);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewSection(SectionDTO s)
        {
            var courseId = HttpContext.Session.GetInt32("CurrentCourseId");
            if (courseId == null)
                return BadRequest("No course selected.");
            s.CourseId=courseId.Value;
            if (ModelState.IsValid)
            {
                var result = await _SS.Create(s);
                if (result != null)
                    return Json(new { success = true, message = "Section created successfully" });
                else
                    return Json(new { success = false, message = "Failed to create Section" });
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
        public async Task<IActionResult> SaveEditSection(SectionDTO s)
        {
            if (ModelState.IsValid)
            {
                var result = await _SS.Update(s);
                if (result != null)
                    return Json(new { success = true, message = "Section updated successfully" });
                else
                    return Json(new { success = false, message = "Failed to udpate section" });
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
        public async Task<IActionResult> AllSections()
        {
            var courseId = HttpContext.Session.GetInt32("CurrentCourseId");
            if (courseId == null)
                return BadRequest("No course selected.");
            
            return PartialView("~/Views/AdminDashboard/ManageSection/_AllSections.cshtml", await _SS.GetList(s => s.CourseId == courseId));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSection(int id)
        {
            if (await _SS.Delete(id))
                return Json(new { success = true, message = "The Section is deleted Successfuly." });
            return Json(new { success = true, message = "The Operation is Failed." });

        }
    }
}
