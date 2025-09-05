
using System.ComponentModel.DataAnnotations;



namespace BLL.DTOS.AnswerOptionDTOS
{
    public class AnswerOptionDTO
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; } = false;
        [Required(ErrorMessage = "The Option Text is Required.")]
        public string OptionText { get; set; }
        [Required(ErrorMessage = "The Question Id is Required.")]
        
        public int QuestionId { get; set; }
    }
}
