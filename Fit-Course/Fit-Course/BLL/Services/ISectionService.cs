using BLL.DTOS.SectionDTOS;
using DAL.Entities.section;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public interface ISectionService
    {
        public Task<List<SectionDTO>> GetList();
        public Task<List<SectionDTO>> GetList(Expression<Func<Section, bool>> filter);
        public Task<SectionDTO?> GetById(int id);
        public Task<Section?> Create(SectionDTO Section);
        public Task<Section?> Update(SectionDTO Section);
        public Task<bool> Delete(int id);
    }
}
