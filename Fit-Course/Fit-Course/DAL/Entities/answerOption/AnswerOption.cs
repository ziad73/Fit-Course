using DAL.Entities.question;
using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.answerOption
{
    public class AnswerOption
    {
        [Key]
        public int Id { get; set; }
        public bool IsCorrect { get; set; } = false;
        [Required(ErrorMessage ="The Option Text is Required.")]
        public string OptionText { get; set; }
        [Required(ErrorMessage = "The Question Id is Required.")]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }=false;
    }
}