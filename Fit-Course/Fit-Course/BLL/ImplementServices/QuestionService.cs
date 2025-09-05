using BLL.DTOS.QuestionDTOs;

using BLL.Mapperly;
using BLL.Services;
using DAL.Entities.question;

using DAL.Entities.user;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


using System.Linq.Expressions;



namespace BLL.ImplementServices
{
    public class QuestionService:IQuestionService
    {
        private readonly IRepository<Question> _QR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        public QuestionService(IRepository<Question> cR, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _QR = cR;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Question?> Create(QuestionDTO Question)
        {
            if (Question == null)
                return null;
            // var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Question mappedQuestion = new QuestionMapper().MapToQuestion(Question);
            mappedQuestion.CreatedOn = DateTime.UtcNow;
            mappedQuestion.CreatedBy = ""/*user.FullName*/;

            await _QR.Create(mappedQuestion);
            return mappedQuestion;
        }


        public async Task<bool> Delete(int id)
        {
            Question t = await _QR.GetByID(id);
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (t == null)
            {
                return false;
            }

            await _QR.Delete(t);
            return true;
        }

        public async Task<QuestionDTO?> GetById(int id)
        {
            Question t = await _QR.GetByID(id);

            if (t == null)
            {
                return null;
            }

            QuestionDTO QuestionDTO = new QuestionMapper().MapToQuestionDTO(t);
            return QuestionDTO;
        }

        public async Task<List<QuestionDTO>> GetList()
        {
            List<Question> Questions = (await _QR.GetAll());

            if (Questions == null || Questions.Count == 0)
            {
                return new List<QuestionDTO>();
            }

            List<QuestionDTO> QuestionsDTO = new QuestionMapper().MapToQuestionDTOList(Questions);
            return QuestionsDTO;
        }
        public async Task<List<QuestionDTO>> GetList(Expression<Func<Question, bool>> filter)
        {
            List<Question> questions = await _QR.GetAllByFilter(filter);

            if (questions == null || questions.Count == 0)
            {
                return new List<QuestionDTO>();
            }

            return new QuestionMapper().MapToQuestionDTOList(questions);
        }
        public async Task<Question?> Update(QuestionDTO Question)
        {
            if (Question == null)
            {
                return null;
            }
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Question UpdateQuestion = await _QR.GetByID(Question.Id);
            UpdateQuestion.QuestionText = Question.QuestionText;
            UpdateQuestion.Mark = Question.Mark;
            UpdateQuestion.ModifiedOn = DateTime.UtcNow;
            UpdateQuestion.ModifiedBy = "" /*user.FullName*/;
            await _QR.Update(UpdateQuestion);
            return UpdateQuestion;
        }
    }
}
