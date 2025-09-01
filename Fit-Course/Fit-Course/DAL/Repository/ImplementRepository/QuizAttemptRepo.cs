using DAL.Database;
using DAL.Entities.quizAttempt;

using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.ImplementRepository
{
    public class QuizAttemptRepo : IRepository<QuizAttempt>
    {
        private readonly FitCourseDb _context;
        public QuizAttemptRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<QuizAttempt> GetByID(int id)
        {
            return await _context.QuizAttempt
                .Include(q=>q.User)
                .Include(q=>q.Quiz)
                
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(QuizAttempt entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(QuizAttempt entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<QuizAttempt>> GetAll()
        {
            return await _context.QuizAttempt
                        .Include(q => q.User)
                        .Include(q => q.Quiz)
                         .ToListAsync();
        }
        public async Task<List<QuizAttempt>> GetAllByFilter(System.Linq.Expressions.Expression<Func<QuizAttempt, bool>> filter)
        {
            return await _context.QuizAttempt
                .Where(filter)
                .Include(q => q.User)
                .Include(q => q.Quiz)
                .ToListAsync();
        }

        public async Task<bool> Update(QuizAttempt entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

      
    }
}
