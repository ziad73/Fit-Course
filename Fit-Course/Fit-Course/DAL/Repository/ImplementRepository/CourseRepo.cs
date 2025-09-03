using DAL.Database;
using DAL.Entities.course;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.ImplementRepository
{
    public class CourseRepo : IRepository<Course>
    {
        private readonly FitCourseDb _context;
        public CourseRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<Course> GetByID(int id)
        {
            return await _context.Course
                .Include(c => c.Sections)
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(Course entity)
         {
            try
            {
                if (entity == null)
                {
                    return null;
                }

                await _context.Course.AddAsync(entity);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex) { 
            Console.WriteLine(ex.ToString());
            }
            return entity.Id;
        }

        public async Task<bool> Delete(Course entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<Course>> GetAll()
        {
            return await _context.Course
                         .Where(r => r.IsDeleted == false)
                         .Include(c => c.Sections)
                         .Include(c => c.Instructor)
                         .Include(c => c.Enrollments)
                         .ToListAsync();
        }
        public async Task<List<Course>> GetAllByFilter(System.Linq.Expressions.Expression<Func<Course, bool>> filter)
        {
            return await _context.Course
                .Where(filter)
                .Include(c => c.Sections)
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .ToListAsync();
        }



        public async Task<bool> Update(Course entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
