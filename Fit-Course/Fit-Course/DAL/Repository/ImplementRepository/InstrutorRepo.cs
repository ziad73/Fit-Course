using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Database;
using DAL.Entities.instructor;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DAL.Entities.user;
namespace DAL.Repository.ImplementRepository
{
    public class InstructorRepo : IRepository<Instructor>
    {
        private readonly FitCourseDb _context;
        public InstructorRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<int?> Create(Instructor entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.InstructorId;

        }

        public async Task<bool> Delete(Instructor entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<Instructor>> GetAll()
        {
            return await _context.Instructor
            .Include(i => i.User)//load User
            .Include(i => i.Courses)
            .ToListAsync();
        }

        public async Task<List<Instructor>> GetAllByFilter(Expression<Func<Instructor, bool>> filter)
        {

            return await _context.Instructor
            .Where(filter)
            .Include(i => i.User)//load User
            .Include(i => i.Courses)
            .ToListAsync();
        }

        public async Task<Instructor> GetByID(int id)
        {
            return await _context.Instructor
            .Include(i => i.User)//load User
            .Include(i => i.Courses)
            .FirstOrDefaultAsync(i => i.InstructorId == id);

        }

        public async Task<bool> Update(Instructor entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
