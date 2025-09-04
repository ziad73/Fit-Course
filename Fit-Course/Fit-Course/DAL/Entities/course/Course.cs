using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.enrollment;
using DAL.Entities.instructor;
using DAL.Entities.section;
using DAL.Entities.user;

namespace DAL.Entities.course
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Title is Required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The Description is Required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The Status is Required.")]

        public string Status { get; set; }
      
       
        

        public Instructor? Instructor { get; set; }//navigation property
                                                   // [Required(ErrorMessage = "The Instructor Id is Required.")]
        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; }
        public List<Section>? Sections { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
       
        
        [Required(ErrorMessage = "The Course Price is Required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Price muset be more than or equal 0")]
        public double Price { get; set; }
        public string? ImagePath { get; set; }
      
      //m to m
        public List<Enrollment> Enrollments { get; set; }
    }
}