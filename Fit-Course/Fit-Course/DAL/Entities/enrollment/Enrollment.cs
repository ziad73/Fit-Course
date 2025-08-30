using DAL.Entities.course;
using DAL.Entities.user;

namespace DAL.Entities.enrollment
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string CoachId { get; set; }
        public User User { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime EnrollDate { get; set; }

    }
}