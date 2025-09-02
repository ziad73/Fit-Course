using BLL.DTOS.CourseDTOS;
using DAL.Entities.course;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapperly
{
    [Mapper]
    public partial class CourseMapper
    {
        public partial CourseDTO MapToCourseDTO(Course Course);
        public partial Course MapToCourse(CourseDTO Course);
        public partial List<CourseDTO> MapToCourseDTOList(List<Course> Coursees);
    }
}
