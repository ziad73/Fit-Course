using BLL.DTOS.SectionDTOS;
using BLL.Helper;
using BLL.Mapperly;
using BLL.Services;
using DAL.Entities.section;

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
    public class SectionService:ISectionService
    {
        private readonly IRepository<Section> _CR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        public SectionService(IRepository<Section> cR, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _CR = cR;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Section?> Create(SectionDTO Section)
        {
            if (Section == null)
                return null;
            // var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Section mappedSection = new SectionMapper().MapToSection(Section);
            mappedSection.CreatedOn = DateTime.UtcNow;
            mappedSection.CreatedBy = ""/*user.FullName*/;
           
            await _CR.Create(mappedSection);
            return mappedSection;
        }


        public async Task<bool> Delete(int id)
        {
            Section t = await _CR.GetByID(id);
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (t == null)
            {
                return false;
            }

            await _CR.Delete(t);
            return true;
        }

        public async Task<SectionDTO?> GetById(int id)
        {
            Section t = await _CR.GetByID(id);

            if (t == null)
            {
                return null;
            }

            SectionDTO SectionDTO = new SectionMapper().MapToSectionDTO(t);
            return SectionDTO;
        }

        public async Task<List<SectionDTO>> GetList()
        {
            List<Section> Sections = (await _CR.GetAll());

            if (Sections == null || Sections.Count == 0)
            {
                return new List<SectionDTO>();
            }

            List<SectionDTO> SectionsDTO = new SectionMapper().MapToSectionDTOList(Sections);
            return SectionsDTO;
        }
        public async Task<List<SectionDTO>> GetList(Expression<Func<Section, bool>> filter)
        {
            List<Section> sections = await _CR.GetAllByFilter(filter);

            if (sections == null || sections.Count == 0)
            {
                return new List<SectionDTO>();
            }

            return new SectionMapper().MapToSectionDTOList(sections);
        }
        public async Task<Section?> Update(SectionDTO Section)
        {
            if (Section == null)
            {
                return null;
            }
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Section UpdateSection = await _CR.GetByID(Section.Id);
           UpdateSection.OrderIndex=Section.OrderIndex;
            UpdateSection.Description = Section.Description;
            UpdateSection.Title = Section.Title;
             UpdateSection.ModifiedOn = DateTime.UtcNow;
            UpdateSection.ModifiedBy = "" /*user.FullName*/;
            await _CR.Update(UpdateSection);
            return UpdateSection;
        }
    }
}

