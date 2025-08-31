using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.section;
using Microsoft.EntityFrameworkCore;
namespace DAL.ImplementRepository
{
    public class SectionRepo : IRepository<Section>
    {
        private readonly FitCourseDb _context;
        public SectionRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<Section> GetByID(int id)
        {
            return await _context.Section
                .Include(s=>s.Course)
                .Include(s=>s.Slide)
                .Include(s=>s.Video)
                .Include(s=>s.Quiz)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(Section entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Section entity)
        {
            if (entity == null)
            {
                return;
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Section>> GetAll()
        {
            return await _context.Section
                         .Where(r => r.IsDeleted == false)
                         .Include(s => s.Course)
                         .Include(s => s.Slide)
                         .Include(s => s.Video)
                         .Include(s => s.Quiz)
                         .ToListAsync();
        }
        public async Task<List<Section>> GetAllByFilter(System.Linq.Expressions.Expression<Func<Section, bool>> filter)
        {
            return await _context.Section
                .Where(filter)
                .Include(s => s.Course)
                .Include(s => s.Slide)
                .Include(s => s.Video)
                .Include(s => s.Quiz)
                .ToListAsync();
        }

        public async Task UpdateAsync(Section entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Section entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
