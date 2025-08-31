using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
   
        public interface IRepository<T> where T : class
        {
            public Task<List<T>> GetAll();
            public Task<T> GetByID(int id);
            public Task<bool> Update(T entity);
            public Task<bool> Delete(T entity);
            public Task<int?> Create(T entity);
            public Task<List<T>> GetAllByFilter(Expression<Func<T, bool>> filter);
        }
   
}
