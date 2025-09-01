using DAL.Database;
using DAL.Entities.answerOption;

using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.ImplementRepository
{
    public class AnswerOptionRepo : IRepository<AnswerOption>
    {
        private readonly FitCourseDb _context;
        public AnswerOptionRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<AnswerOption> GetByID(int id)
        {
            return await _context.AnswerOption
                .Include(a=>a.Question)
                
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(AnswerOption entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(AnswerOption entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<AnswerOption>> GetAll()
        {
            return await _context.AnswerOption
                        .Include(a => a.Question)

                         .ToListAsync();
        }
        public async Task<List<AnswerOption>> GetAllByFilter(System.Linq.Expressions.Expression<Func<AnswerOption, bool>> filter)
        {
            return await _context.AnswerOption
                .Where(filter)
                .Include(a => a.Question)

                .ToListAsync();
        }

       

        public async Task<bool> Update(AnswerOption entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
