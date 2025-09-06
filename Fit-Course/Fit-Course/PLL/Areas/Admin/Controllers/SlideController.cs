using BLL.DTOS.SlideDTOS;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PLL.Areas.Admin.ViewsModels.SlideVM;


namespace PLL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        private readonly ISlideService _SS;

        public SlideController(ISlideService sS)
        {
            _SS = sS;
        }
        [HttpGet]
        public IActionResult Index(int sectionId)
        {
            HttpContext.Session.SetInt32("CurrentsectionId", sectionId);

            return View("~/Areas/Admin/Views/ManageSlide/Index.cshtml");
        }
        public IActionResult AddSlide()
        {
            return PartialView("~/Areas/Admin/Views/ManageSlide/_AddSlide.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> EditSlide(int SlideId)
        {
            SlideDTO s = new SlideDTO();
            s = await _SS.GetById(SlideId);
            if (s == null)
                return BadRequest("No Slide selected.");
            return PartialView("~/Areas/Admin/Views/ManageSlide/_EditSlide.cshtml", s);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewSlide(SlideDTO s)
        {
            var sectionId = HttpContext.Session.GetInt32("CurrentsectionId");
            if (sectionId == null)
                return BadRequest("No section selected.");
            s.SectionId = sectionId.Value;
            if (ModelState.IsValid)
            {
                var result = await _SS.Create(s);
                if (result != null)
                    return Json(new { success = true, message = "Slide created successfully" });
                else
                    return Json(new { success = false, message = "Failed to create Slide" });
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
        public async Task<IActionResult> SaveEditSlide(SlideDTO s)
        {
            if (ModelState.IsValid)
            {
                var result = await _SS.Update(s);
                if (result != null)
                    return Json(new { success = true, message = "Slide updated successfully" });
                else
                    return Json(new { success = false, message = "Failed to udpate Slide" });
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
        public async Task<IActionResult> AllSlides(int pageSize = 3, int pageNumber = 1)
        {
            var sectionId = HttpContext.Session.GetInt32("CurrentsectionId");
            if (sectionId == null)
                return BadRequest("No section selected.");

            var Slides = await _SS.GetList(s=>s.SectionId==sectionId);

            var pagedSlides = Slides
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalsections = Slides.Count;

            return PartialView("~/Areas/Admin/Views/ManageSlide/_AllSlides.cshtml", new PagedSlides
            {
                Slides = pagedSlides,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalsections / (double)pageSize)
            });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteSlide(int id)
        {
            if (await _SS.Delete(id))
                return Json(new { success = true, message = "The Slide is deleted Successfuly." });
            return Json(new { success = true, message = "The Operation is Failed." });

        }
    }
}
