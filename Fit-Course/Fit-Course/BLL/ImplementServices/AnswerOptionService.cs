using BLL.DTOS.AnswerOptionDTOS;
using BLL.Helper;
using BLL.Mapperly;
using BLL.Services;
using DAL.Entities.answerOption;

using DAL.Entities.user;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ImplementServices
{
    public class AnswerOptionService:IAnswerOptionService
    {
        private readonly IRepository<AnswerOption> _SR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        public AnswerOptionService(IRepository<AnswerOption> cR, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _SR = cR;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<AnswerOption?> Create(AnswerOptionDTO AnswerOption)
        {
            if (AnswerOption == null)
                return null;
            // var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            AnswerOption mappedAnswerOption = new AnswerOptionMapper().MapToAnswerOption(AnswerOption);
            mappedAnswerOption.CreatedOn = DateTime.UtcNow;
            mappedAnswerOption.CreatedBy = ""/*user.FullName*/;

            await _SR.Create(mappedAnswerOption);
            return mappedAnswerOption;
        }


        public async Task<bool> Delete(int id)
        {
            AnswerOption t = await _SR.GetByID(id);
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (t == null)
            {
                return false;
            }

            await _SR.Delete(t);
            return true;
        }

        public async Task<AnswerOptionDTO?> GetById(int id)
        {
            AnswerOption t = await _SR.GetByID(id);

            if (t == null)
            {
                return null;
            }

            AnswerOptionDTO AnswerOptionDTO = new AnswerOptionMapper().MapToAnswerOptionDTO(t);
            return AnswerOptionDTO;
        }

        public async Task<List<AnswerOptionDTO>> GetList()
        {
            List<AnswerOption> AnswerOptions = (await _SR.GetAll());

            if (AnswerOptions == null || AnswerOptions.Count == 0)
            {
                return new List<AnswerOptionDTO>();
            }

            List<AnswerOptionDTO> AnswerOptionsDTO = new AnswerOptionMapper().MapToAnswerOptionDTOList(AnswerOptions);
            return AnswerOptionsDTO;
        }
        public async Task<List<AnswerOptionDTO>> GetList(Expression<Func<AnswerOption, bool>> filter)
        {
            List<AnswerOption> AnswerOptions = await _SR.GetAllByFilter(filter);

            if (AnswerOptions == null || AnswerOptions.Count == 0)
            {
                return new List<AnswerOptionDTO>();
            }

            return new AnswerOptionMapper().MapToAnswerOptionDTOList(AnswerOptions);
        }
        public async Task<AnswerOption?> Update(AnswerOptionDTO AnswerOption)
        {
            if (AnswerOption == null)
            {
                return null;
            }
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            AnswerOption UpdateAnswerOption = await _SR.GetByID(AnswerOption.Id);
           
            UpdateAnswerOption.OptionText = AnswerOption.OptionText;
            UpdateAnswerOption.IsCorrect = AnswerOption.IsCorrect;
            
            UpdateAnswerOption.ModifiedOn = DateTime.UtcNow;
            UpdateAnswerOption.ModifiedBy = "" /*user.FullName*/;
            await _SR.Update(UpdateAnswerOption);
            return UpdateAnswerOption;
        }
    }
}
