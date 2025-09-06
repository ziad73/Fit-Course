using DAL.Entities.answerOption;
using DAL.Entities.quiz;
using DAL.Entities.qustionType;
using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.question
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The Type is Required.")]
        public QustionType Type { get; set; }
        [Required(ErrorMessage ="Question Text is Required.")]
        public string QuestionText { get; set; }
        [Required(ErrorMessage ="The Mark is Required.")]
        public double Mark { get; set; }
        [Required(ErrorMessage = "The Quiz Id is Required.")]
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public List<AnswerOption> answerOptions { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
       
    }
}