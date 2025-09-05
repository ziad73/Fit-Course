
using BLL.DTOS.QuestionDTOs;
using DAL.Entities.question;
using Riok.Mapperly.Abstractions;


namespace BLL.Mapperly
{
    [Mapper]
    public partial class QuestionMapper
    {
        public partial QuestionDTO MapToQuestionDTO(Question Question);
        public partial Question MapToQuestion(QuestionDTO Question);
        public partial List<QuestionDTO> MapToQuestionDTOList(List<Question> Questiones);
    }
}
