using DAL.Entities.quiz;
using DAL.Entities.qustionType;
using DAL.Entities.section;
using DAL.Entities.user;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.quizAttempt
{
    public class QuizAttempt
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The Score is Required.")]
        [Range(0,double.MaxValue, ErrorMessage = "The Score must be more than or equal 0.")]
        public decimal Score { get; set; }

        [Required(ErrorMessage ="The Coach Id is Required.")]
        [ForeignKey("User")]
        public string CoachId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage ="The Quiz Id is Required.")]
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        [Required(ErrorMessage ="The Quiz Status is Required.")]
        public TestStatus QuizStatus { get; set; }
       
    }
}