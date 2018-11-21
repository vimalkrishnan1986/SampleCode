using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Education.Data.Common
{
    public abstract class BaseRepository<T> : IGenericRepository<T>
        where T : class, new()
    {
        protected DbContext _dbContext { get; set; }

        protected BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> GetAsync(string id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
