using BLL.DTOS.SlideDTOS;
using DAL.Entities.slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ISlideService
    {
        public Task<List<SlideDTO>> GetList();
        public Task<List<SlideDTO>> GetList(Expression<Func<Slide, bool>> filter);
        public Task<SlideDTO?> GetById(int id);
        public Task<Slide?> Create(SlideDTO Slide);
        public Task<Slide?> Update(SlideDTO Slide);
        public Task<bool> Delete(int id);
    }
}
