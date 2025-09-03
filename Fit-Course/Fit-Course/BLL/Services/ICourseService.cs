using BLL.DTOS.CourseDTOS;
using DAL.Entities.course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICourseService
    {
        public Task<List<CourseDTO>> GetList();
        public  Task<List<CourseDTO>> GetList(Expression<Func<Course, bool>> filter);
        public Task<CourseDTO?> GetById(int id);
        public Task<Course?> Create(CourseDTO Course);
        public Task<Course?> Update(CourseDTO Course);
        public Task<bool> Delete(int id);
        public  Task<bool> Draft(int id);
    }
}
