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
        public async Task<IActionResult> AddCourse(ManageCourseDTO mc)
        {
            ManageCourseDTO courseDTO = new ManageCourseDTO();
            var usersInRole = await _userManager.GetUsersInRoleAsync("Instructor");
            courseDTO.Instructors =usersInRole.ToList();
            return PartialView("_AddCourse", mc);
        }
        public async Task<IActionResult> SaveNewCourse(ManageCourseDTO mc)
        {
            if (ModelState.IsValid) 
            {
                if (_CS.Create(mc.CourseDTO) != null)
                    return RedirectToAction("Index");

            }
           return RedirectToAction("AddCourse",mc);
        }
    }
}
