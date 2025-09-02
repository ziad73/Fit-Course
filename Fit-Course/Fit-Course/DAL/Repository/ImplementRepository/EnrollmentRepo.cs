using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Database;
using DAL.Entities.enrollment;
namespace DAL.Repository.ImplementRepository
{
    public class EnrollmentRepo : IRepository<Enrollment>
    {
        private readonly FitCourseDb _context;
        public EnrollmentRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<Enrollment> GetByID(int id)
        {
            return await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.User)
                .Include(e => e.Payment)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(Enrollment entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(Enrollment entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<Enrollment>> GetAll()
        {
            return await _context.Enrollment
                        .Include(e => e.Course)
                        .Include(e => e.User)
                .Include(e => e.Payment)

                         .ToListAsync();
        }
        public async Task<List<Enrollment>> GetAllByFilter(System.Linq.Expressions.Expression<Func<Enrollment, bool>> filter)
        {
            return await _context.Enrollment
                .Where(filter)
                        .Include(e => e.Course)
                        .Include(e => e.User)
                .Include(e => e.Payment)

                .ToListAsync();
        }

        public async Task<bool> Update(Enrollment entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
