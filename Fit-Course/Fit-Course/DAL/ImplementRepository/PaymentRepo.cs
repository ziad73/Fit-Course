using DAL.Entities;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ImplementRepository
{
    public class PaymentRepo : IRepository<Payment>
    {
        private readonly FitCourseDb _context;
        public PaymentRepo(FitCourseDb context)
        {
            _context = context;
        }
        public async Task<Payment> GetByID(int id)
        {
            return await _context.Payment
                .FirstOrDefaultAsync(c => c.PaymentID == id);
        }

        public async Task<int?> Create(Payment entity)
        {
            if (entity == null)
            {
                return null;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.PaymentID;
        }

        public async Task<bool> Delete(Payment entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<List<Payment>> GetAll()
        {
            return await _context.Payment
                        
                         .ToListAsync();
        }
        public async Task<List<Payment>> GetAllByFilter(System.Linq.Expressions.Expression<Func<Payment, bool>> filter)
        {
            return await _context.Payment
                .Where(filter)
                .ToListAsync();
        }

      

        public async Task<bool> Update(Payment entity)
        {
            _context.Update(entity);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
