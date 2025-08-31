using DAL.Entities.course;
using DAL.Entities.user;
using Resturant_DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.enrollment
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The User Id is Required.")]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "The Course Id is Required.")]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage ="EnrollMent Date is Required.")]
        public DateTime EnrollemntDate { get; set; }
        [Required(ErrorMessage ="The Payment is Required.")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}