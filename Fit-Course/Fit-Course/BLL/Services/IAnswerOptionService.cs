
using BLL.DTOS.AnswerOptionDTOS;
using DAL.Entities.answerOption;
using System.Linq.Expressions;


namespace BLL.Services
{
    public interface IAnswerOptionService
    {
        public Task<List<AnswerOptionDTO>> GetList();
        public Task<List<AnswerOptionDTO>> GetList(Expression<Func<AnswerOption, bool>> filter);
        public Task<AnswerOptionDTO?> GetById(int id);
        public Task<AnswerOption?> Create(AnswerOptionDTO AnswerOption);
        public Task<AnswerOption?> Update(AnswerOptionDTO AnswerOption);
        public Task<bool> Delete(int id);

    }
}
