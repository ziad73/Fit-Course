using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.section;
using Microsoft.EntityFrameworkCore;
using DAL.Database;
namespace DAL.Repository.ImplementRepository
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
                .Include(s => s.Course)
                .Include(s => s.Slide)
                .Include(s => s.Video)
                .Include(s => s.Quiz)
                .Include(s => s.Progresses)
               
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

        public async Task<bool> Delete(Section entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<Section>> GetAll()
        {
            return await _context.Section
                      
                         .Include(s => s.Course)
                         .Include(s => s.Slide)
                         .Include(s => s.Video)
                .Include(s => s.Progresses)
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
                .Include(s => s.Progresses)
                .Include(s => s.Quiz)
                .ToListAsync();
        }

        public async Task<bool> Update(Section entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
