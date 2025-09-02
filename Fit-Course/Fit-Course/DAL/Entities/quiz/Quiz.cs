using DAL.Entities.question;
using DAL.Entities.quizAttempt;
using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.quiz
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Title is Required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Section Id is Required.")]
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }

        public List<Question> Question { get; set; }
        [Required(ErrorMessage = "The Max Mark is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "The Max Mark must be more than or equal 1.")]
        public double MaxMark { get; set; }
        [Required(ErrorMessage = "The Min Mark is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "The Min Mark must be more than or equal 1.")]

        public double MinMark { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public List<QuizAttempt> quizAttempts { get; set; }
    }
}