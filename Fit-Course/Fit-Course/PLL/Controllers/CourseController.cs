using BLL.DTOS.CourseDTOS;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using DAL.Entities.user;
namespace PLL.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _CS;
        private readonly UserManager<User> _userManager;
        public CourseController(ICourseService cS, UserManager<User> userManager)
        {
            _CS = cS;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            
            return View("Index");
        }
        public IActionResult AddCourse()
        {
            return PartialView("~/Views/Shared/Course/_AddCourse.cshtml");
        }
        [HttpGet]
        public async  Task<IActionResult> EditCourse(int courseId)
        {
            ManageCourseDTO mc=new ManageCourseDTO();
            mc.CourseDTO = await _CS.GetById(courseId);
            return PartialView("~/Views/Shared/Course/_EditCourse.cshtml",mc);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewCourse(ManageCourseDTO mc)
        {
            if (ModelState.IsValid) 
            {
                var result = await _CS.Create(mc.CourseDTO);
                if (result != null)
                    return Json(new { success = true, message = "Course created successfully" });
                else
                    return Json(new { success = false, message = "Failed to create course" });
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
        public async Task<IActionResult> SaveEditCourse(ManageCourseDTO mc)
        {
            if (ModelState.IsValid)
            {
                var result = await _CS.Update(mc.CourseDTO);
                if (result != null)
                    return Json(new { success = true, message = "Course updated successfully" });
                else
                    return Json(new { success = false, message = "Failed to udpate course" });
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

        public async Task<IActionResult> AllCourses()
        {
            return PartialView("~/Views/Shared/Course/_AllCourses.cshtml", await _CS.GetList(c=>c.IsDeleted==true || c.IsDeleted==false));
        }
        public async Task<IActionResult> AllCoursesDraft()
        {
            return PartialView("~/Views/Shared/Course/_AllCourses.cshtml", await _CS.GetList(c => c.IsDeleted == true ));
        }
        public async Task<IActionResult> AllCoursesPublished()
        {
            return PartialView("~/Views/Shared/Course/_AllCourses.cshtml", await _CS.GetList(c =>  c.IsDeleted == false));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if(await _CS.Delete(id))
                return Json(new { success = true,message="The Course is deleted Successfuly." } );
            return Json(new { success = true, message = "The Operation is Failed." });

        }
    }
}
