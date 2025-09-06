using BLL.DTOS.CourseDTOS;

namespace PLL.Areas.Admin.ViewsModels.CourseVM
{
    public class PagedCourses
    {
        public List<CourseDTO> Courses { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
