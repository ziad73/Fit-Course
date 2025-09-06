
using BLL.DTOS.QuizDTOS;
using BLL.Helper;
using BLL.Mapperly;
using BLL.Services;
using DAL.Entities.quiz;

using DAL.Entities.user;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using System.Linq.Expressions;

namespace BLL.ImplementServices
{
    public class QuizService:IQuizService
    {
        private readonly IRepository<Quiz> _QR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        public QuizService(IRepository<Quiz> cR, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _QR = cR;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Quiz?> Create(QuizDTO Quiz)
        {
            if (Quiz == null)
                return null;
            // var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Quiz mappedQuiz = new QuizMapper().MapToQuiz(Quiz);
            mappedQuiz.CreatedOn = DateTime.UtcNow;
            mappedQuiz.CreatedBy = ""/*user.FullName*/;

            await _QR.Create(mappedQuiz);
            return mappedQuiz;
        }


        public async Task<bool> Delete(int id)
        {
            Quiz t = await _QR.GetByID(id);
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (t == null)
            {
                return false;
            }

            await _QR.Delete(t);
            return true;
        }

        public async Task<QuizDTO?> GetById(int id)
        {
            Quiz t = await _QR.GetByID(id);

            if (t == null)
            {
                return null;
            }

            QuizDTO QuizDTO = new QuizMapper().MapToQuizDTO(t);
            return QuizDTO;
        }

        public async Task<List<QuizDTO>> GetList()
        {
            List<Quiz> Quizs = (await _QR.GetAll());

            if (Quizs == null || Quizs.Count == 0)
            {
                return new List<QuizDTO>();
            }

            List<QuizDTO> QuizsDTO = new QuizMapper().MapToQuizDTOList(Quizs);
            return QuizsDTO;
        }
        public async Task<List<QuizDTO>> GetList(Expression<Func<Quiz, bool>> filter)
        {
            List<Quiz> quizes = await _QR.GetAllByFilter(filter);

            if (quizes == null || quizes.Count == 0)
            {
                return new List<QuizDTO>();
            }

            return new QuizMapper().MapToQuizDTOList(quizes);
        }
        public async Task<Quiz?> Update(QuizDTO Quiz)
        {
            if (Quiz == null)
            {
                return null;
            }
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Quiz UpdateQuiz = await _QR.GetByID(Quiz.Id);
            UpdateQuiz.Title = Quiz.Title;
            UpdateQuiz.MaxMark = Quiz.MaxMark;
            UpdateQuiz.MinMark = Quiz.MinMark;
            UpdateQuiz.ModifiedOn = DateTime.UtcNow;
            UpdateQuiz.ModifiedBy = "" /*user.FullName*/;
            await _QR.Update(UpdateQuiz);
            return UpdateQuiz;
        }
    }
}

