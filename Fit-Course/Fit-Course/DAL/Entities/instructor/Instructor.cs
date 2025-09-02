using DAL.Entities.course;
using DAL.Entities.user;

namespace DAL.Entities.instructor
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; } //navigation property

        public string Bio { get; set; }
        public string Expertise { get; set; }

        public List<Course>? Courses { get; set; }//nav property


    }
}