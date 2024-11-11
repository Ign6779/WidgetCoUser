using WidgetCoUser.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WidgetCoUser.Services.Interfaces
{
    public interface IService<T> where T : class, IEntity
    {
        public Task<T> GetByIdAsync(string id);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

        public Task<T> CreateAsync(T entity);

        public Task<T> UpdateAsync(T entity);

        public Task DeleteAsync(string id);
    }
}