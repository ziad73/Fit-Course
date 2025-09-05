

using BLL.DTOS.SlideDTOS;
using BLL.Helper;
using BLL.Mapperly;
using BLL.Services;
using DAL.Entities.slide;
using DAL.Entities.user;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace BLL.ImplementServices
{
    public class SlideService:ISlideService
    {
        private readonly IRepository<Slide> _SR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> userManager;
        public SlideService(IRepository<Slide> cR, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _SR = cR;
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Slide?> Create(SlideDTO Slide)
        {
            if (Slide == null)
                return null;
            // var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Slide mappedSlide = new SlideMapper().MapToSlide(Slide);
            mappedSlide.CreatedOn = DateTime.UtcNow;
            mappedSlide.CreatedBy = ""/*user.FullName*/;

            await _SR.Create(mappedSlide);
            return mappedSlide;
        }


        public async Task<bool> Delete(int id)
        {
            Slide t = await _SR.GetByID(id);
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (t == null)
            {
                return false;
            }

            await _SR.Delete(t);
            return true;
        }

        public async Task<SlideDTO?> GetById(int id)
        {
            Slide t = await _SR.GetByID(id);

            if (t == null)
            {
                return null;
            }

            SlideDTO SlideDTO = new SlideMapper().MapToSlideDTO(t);
            return SlideDTO;
        }

        public async Task<List<SlideDTO>> GetList()
        {
            List<Slide> Slides = (await _SR.GetAll());

            if (Slides == null || Slides.Count == 0)
            {
                return new List<SlideDTO>();
            }

            List<SlideDTO> SlidesDTO = new SlideMapper().MapToSlideDTOList(Slides);
            return SlidesDTO;
        }
        public async Task<List<SlideDTO>> GetList(Expression<Func<Slide, bool>> filter)
        {
            List<Slide> slides = await _SR.GetAllByFilter(filter);

            if (slides == null || slides.Count == 0)
            {
                return new List<SlideDTO>();
            }

            return new SlideMapper().MapToSlideDTOList(slides);
        }
        public async Task<Slide?> Update(SlideDTO Slide)
        {
            if (Slide == null)
            {
                return null;
            }
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Slide UpdateSlide = await _SR.GetByID(Slide.Id);
            UpdateSlide.FilePath = Slide.FilePath;
            var type = FileHelper.GetFileTypeFromPath(Slide.FilePath);
            if (type == null) return null;
            UpdateSlide.Type = type.Value;
            UpdateSlide.ModifiedOn = DateTime.UtcNow;
            UpdateSlide.ModifiedBy = "" /*user.FullName*/;
            await _SR.Update(UpdateSlide);
            return UpdateSlide;
        }
    }
}
