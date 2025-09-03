using DAL.Entities.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOS.CourseDTOS
{
    public class ManageCourseDTO
    {
        public CourseDTO CourseDTO { get; set; }
        //public List<User> Instructors { get; set; }
    }
}
