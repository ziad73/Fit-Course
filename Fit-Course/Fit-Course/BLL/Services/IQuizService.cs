using BLL.DTOS.QuizDTOS;
using DAL.Entities.quiz;

using System.Linq.Expressions;


namespace BLL.Services
{
    public interface IQuizService
    {
        public Task<List<QuizDTO>> GetList();
        public Task<List<QuizDTO>> GetList(Expression<Func<Quiz, bool>> filter);
        public Task<QuizDTO?> GetById(int id);
        public Task<Quiz?> Create(QuizDTO Quiz);
        public Task<Quiz?> Update(QuizDTO Quiz);
        public Task<bool> Delete(int id);
    }
}
