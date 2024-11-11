using WidgetCoUser.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WidgetCoUser.Repositories.Interfaces
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity //to use Id property
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity.Id == null)
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entry = _context.Add(entity); 
            entry.State = EntityState.Unchanged;

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);

            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}