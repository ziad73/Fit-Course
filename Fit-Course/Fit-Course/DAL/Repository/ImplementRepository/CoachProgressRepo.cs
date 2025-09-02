using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Database;
using DAL.Entities.coachProgress;
namespace DAL.Repository.ImplementRepository
{
    public class CoachProgressRepo : IRepository<CoachProgress>
    {
        private readonly FitCourseDb _context;
        public CoachProgressRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<CoachProgress> GetByID(int id)
        {
            return await _context.CoachProgress
                .Include(c => c.Section)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(CoachProgress entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(CoachProgress entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<CoachProgress>> GetAll()
        {
            return await _context.CoachProgress
                .Include(c => c.Section)
                .Include(c => c.User)
                         .ToListAsync();
        }
        public async Task<List<CoachProgress>> GetAllByFilter(System.Linq.Expressions.Expression<Func<CoachProgress, bool>> filter)
        {
            return await _context.CoachProgress
                .Where(filter)
                .Include(c => c.Section)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<bool> Update(CoachProgress entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
