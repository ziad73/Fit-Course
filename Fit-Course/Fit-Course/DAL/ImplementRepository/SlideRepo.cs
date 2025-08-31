using DAL.Entities.slide;

using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ImplementRepository
{
    public class SlideRepo : IRepository<Slide>
    {
        private readonly FitCourseDb _context;
        public SlideRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<Slide> GetByID(int id)
        {
            return await _context.Slide
                .Include(s=>s.Section)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(Slide entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Slide entity)
        {
            if (entity == null)
            {
                return;
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Slide>> GetAll()
        {
            return await _context.Slide
                         .Where(r => r.IsDeleted == false)
                         .Include(s => s.Section)

                         .ToListAsync();
        }
        public async Task<List<Slide>> GetAllByFilter(System.Linq.Expressions.Expression<Func<Slide, bool>> filter)
        {
            return await _context.Slide
                .Where(filter)
                .Include(s => s.Section)
                .ToListAsync();
        }

        public async Task UpdateAsync(Slide entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Slide entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
