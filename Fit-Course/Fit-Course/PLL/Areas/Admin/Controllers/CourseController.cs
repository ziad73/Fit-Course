using BLL.DTOS.CourseDTOS;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using DAL.Entities.user;
using PLL.Areas.Admin.ViewsModels.CourseVM;
namespace PLL.Controllers
{
    [Area("Admin")]
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
            
            return View("~/Areas/Admin/Views/ManageCourse/Index.cshtml");
        }
        public IActionResult AddCourse()
        {
            return PartialView("~/Areas/Admin/Views/ManageCourse/_AddCourse.cshtml");
        }
        [HttpGet]
        public async  Task<IActionResult> EditCourse(int courseId)
        {
            ManageCourseDTO mc=new ManageCourseDTO();
            mc.CourseDTO = await _CS.GetById(courseId);
            return PartialView("~/Areas/Admin/Views/ManageCourse/_EditCourse.cshtml", mc);
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
        [HttpGet]
        public async Task<IActionResult> AllCourses(int pageSize=3,int pageNumber=1)
        {
            var courses =await _CS.GetList(); 

            var pagedCourses = courses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalCourses = courses.Count;

            return PartialView("~/Areas/Admin/Views/ManageCourse/_AllCourses.cshtml", new PagedCourses
            {
                Courses = pagedCourses,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCourses / (double)pageSize)
            });
         }
        [HttpGet]
        public async Task<IActionResult> AllCoursesDraft(int pageSize = 3, int pageNumber = 1)
        {
            var courses = await _CS.GetList(c => c.Status == "draft");

            var pagedCourses = courses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalCourses = courses.Count;

            return PartialView("~/Areas/Admin/Views/ManageCourse/_AllCourses.cshtml", new PagedCourses
            {
                Courses = pagedCourses,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCourses / (double)pageSize)
            });
           }
        [HttpGet]
        public async Task<IActionResult> AllCoursesPublished(int pageSize = 3, int pageNumber = 1)
        {
            var courses = await _CS.GetList(c => c.Status == "published");

            var pagedCourses = courses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalCourses = courses.Count;

            return PartialView("~/Areas/Admin/Views/ManageCourse/_AllCourses.cshtml", new PagedCourses
            {
                Courses = pagedCourses,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCourses / (double)pageSize)
            });
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
