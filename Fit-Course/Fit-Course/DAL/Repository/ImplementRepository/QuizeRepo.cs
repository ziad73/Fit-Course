using DAL.Database;
using DAL.Entities.quiz;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.ImplementRepository
{
    public class QuizRepo : IRepository<Quiz>
    {
        private readonly FitCourseDb _context;
        public QuizRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<Quiz> GetByID(int id)
        {
            return await _context.Quiz
                .Include(q=>q.Section)
                .Include(q=>q.Question)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(Quiz entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(Quiz entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<Quiz>> GetAll()
        {
            return await _context.Quiz
                         .Where(r => r.IsDeleted == false)
                         .Include(q => q.Section)
                .Include(q => q.Question)

                         .ToListAsync();
        }
        public async Task<List<Quiz>> GetAllByFilter(System.Linq.Expressions.Expression<Func<Quiz, bool>> filter)
        {
            return await _context.Quiz
                .Where(filter)
                .Include(q => q.Section)
                .Include(q => q.Question)
                .ToListAsync();
        }

        public async Task<bool> Update(Quiz entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

      
    }
}
