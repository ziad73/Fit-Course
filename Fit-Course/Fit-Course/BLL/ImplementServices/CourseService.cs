using BLL.DTOS.CourseDTOS;
using BLL.Helper;
using BLL.Mapperly;
using BLL.Services;
using DAL.Entities.course;
using DAL.Entities.user;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ImplementServices
{
    public class CourseService:ICourseService
    {
        private readonly IRepository<Course> _CR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        public CourseService(IRepository<Course> cR, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _CR = cR;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Course?> Create(CourseDTO Course)
        {
            if (Course == null)
                return null;
           // var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Course mappedCourse = new CourseMapper().MapToCourse(Course);
            mappedCourse.CreatedOn = DateTime.UtcNow;
            mappedCourse.CreatedBy = ""/*user.FullName*/;
            mappedCourse.ImagePath= Course.ImageUrl!=null? Upload.UploadFile("CourseImages", Course.ImageUrl):null;
            mappedCourse.InstructorId= Course.InstructorId != null ? Course.InstructorId : null;
            if(Course.Status=="Published")
              mappedCourse.IsDeleted = false;
            else mappedCourse.IsDeleted = true;
            await _CR.Create(mappedCourse);
            return mappedCourse;
        }

       
        public async Task<bool> Delete(int id)
        {
            Course t = await _CR.GetByID(id);
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (t == null )
            {
                return false;
            }
           
            await _CR.Delete(t);
            return true;
        }

        public async Task<CourseDTO?> GetById(int id)
        {
            Course t = await _CR.GetByID(id);

            if (t == null )
            {
                return null;
            }

            CourseDTO CourseDTO = new CourseMapper().MapToCourseDTO(t);
            return CourseDTO;
        }

        public async Task<List<CourseDTO>> GetList()
        {
            List<Course> Courses = (await _CR.GetAll()).Where(t => t.IsDeleted == false).ToList();

            if (Courses == null || Courses.Count == 0)
            {
                return new List<CourseDTO>();
            }

            List<CourseDTO> CoursesDTO = new CourseMapper().MapToCourseDTOList(Courses);
            return CoursesDTO;
        }
        public async Task<List<CourseDTO>> GetList(Expression<Func<Course, bool>> filter)
        {
            List<Course> chiefs = await _CR.GetAllByFilter(filter);

            if (chiefs == null || chiefs.Count == 0)
            {
                return new List<CourseDTO>();
            }

            return new CourseMapper().MapToCourseDTOList(chiefs);
        }
        public async Task<Course?> Update(CourseDTO Course)
        {
            if (Course == null)
            {
                return null;
            }
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Course UpdateCourse = await _CR.GetByID(Course.Id);
            UpdateCourse.Status = Course.Status;
            UpdateCourse.Description = Course.Description;
            UpdateCourse.Title = Course.Title;
            UpdateCourse.Price = Course.Price;
            UpdateCourse.InstructorId = Course.InstructorId!=null ? Course.InstructorId:null;
            UpdateCourse.ModifiedOn = DateTime.UtcNow;
            UpdateCourse.ModifiedBy ="" /*user.FullName*/;
            if (Course.Status == "published")
                UpdateCourse.IsDeleted = false;
            else UpdateCourse.IsDeleted = true;
            await _CR.Update(UpdateCourse);
            return UpdateCourse;
        }
    }
}
