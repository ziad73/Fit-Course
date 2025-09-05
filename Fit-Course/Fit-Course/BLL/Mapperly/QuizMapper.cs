
using BLL.DTOS.QuizDTOS;
using DAL.Entities.quiz;
using Riok.Mapperly.Abstractions;

namespace BLL.Mapperly
{
    [Mapper]
    public partial class QuizMapper
    {
        public partial QuizDTO MapToQuizDTO(Quiz Quiz);
        public partial Quiz MapToQuiz(QuizDTO Quiz);
        public partial List<QuizDTO> MapToQuizDTOList(List<Quiz> Quizes);
    }
}
