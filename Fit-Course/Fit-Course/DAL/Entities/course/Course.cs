using System.ComponentModel.DataAnnotations;
using DAL.Entities.section;
using DAL.Entities.user;

namespace DAL.Entities.course
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Category { get; set; }
        public DateTime CreatedDate { get; set; }

        public User Instructor { get; set; }
        public string InstructorId { get; set; }//navigation property 


        public List<Section> Sections { get; set; }

    }
}