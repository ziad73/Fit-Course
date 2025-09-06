

using BLL.DTOS.QuestionDTOs;
using DAL.Entities.question;
using System.Linq.Expressions;


namespace BLL.Services
{
    public interface IQuestionService
    {
        public Task<List<QuestionDTO>> GetList();
        public Task<List<QuestionDTO>> GetList(Expression<Func<Question, bool>> filter);
        public Task<QuestionDTO?> GetById(int id);
        public Task<Question?> Create(QuestionDTO Question);
        public Task<Question?> Update(QuestionDTO Question);
        public Task<bool> Delete(int id);
    }
}
