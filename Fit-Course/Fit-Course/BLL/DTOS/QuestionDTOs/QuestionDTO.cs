using DAL.Entities.qustionType;

using System.ComponentModel.DataAnnotations;


namespace BLL.DTOS.QuestionDTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Type is Required.")]
        public QustionType Type { get; set; }
        [Required(ErrorMessage = "Question Text is Required.")]
        public string QuestionText { get; set; }
        [Required(ErrorMessage = "The Mark is Required.")]
        public double Mark { get; set; }
        [Required(ErrorMessage = "The Quiz Id is Required.")]
        
        public int QuizId { get; set; }
    }
}
