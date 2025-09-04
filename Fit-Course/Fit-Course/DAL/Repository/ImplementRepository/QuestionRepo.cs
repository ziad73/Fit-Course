using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Database;
using DAL.Entities.question;
namespace DAL.Repository.ImplementRepository
{
    public class QuestionRepo : IRepository<Question>
    {
        private readonly FitCourseDb _context;
        public QuestionRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<Question> GetByID(int id)
        {
            return await _context.Question
                .Include(q => q.Quiz)
                .Include(q => q.answerOptions)
               
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(Question entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(Question entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<Question>> GetAll()
        {
            return await _context.Question
                        
                .Include(q => q.Quiz)
                .Include(q => q.answerOptions)
                         .ToListAsync();
        }
        public async Task<List<Question>> GetAllByFilter(System.Linq.Expressions.Expression<Func<Question, bool>> filter)
        {
            return await _context.Question
                .Where(filter)
                .Include(q => q.Quiz)
                .Include(q => q.answerOptions)
                .ToListAsync();
        }

        public async Task<bool> Update(Question entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
