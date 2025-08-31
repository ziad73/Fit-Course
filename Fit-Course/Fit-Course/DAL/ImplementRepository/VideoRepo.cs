using DAL.Entities.video;

using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ImplementRepository
{
    public class VideoRepo : IRepository<Video>
    {
        private readonly FitCourseDb _context;
        public VideoRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<Video> GetByID(int id)
        {
            return await _context.Video
                
                .Include(v=>v.Section)
                
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Create(Video entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Video entity)
        {
            if (entity == null)
            {
                return;
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Video>> GetAll()
        {
            return await _context.Video
                         .Where(r => r.IsDeleted == false)

                         .Include(v => v.Section)


                         .ToListAsync();
        }
        public async Task<List<Video>> GetAllByFilter(System.Linq.Expressions.Expression<Func<Video, bool>> filter)
        {
            return await _context.Video
                .Where(filter)
                .Include(v => v.Section)

                .ToListAsync();
        }

        public async Task UpdateAsync(Video entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Video entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
