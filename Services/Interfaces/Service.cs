using WidgetCoUser.Models.Interfaces;
using WidgetCoUser.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WidgetCoUser.Services.Interfaces
{
    public abstract class Service<T> : IService<T> where T : class, IEntity
    {
        protected readonly IRepository<T> _repository;

        protected Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync();

        public virtual async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate) => await _repository.GetWhereAsync(predicate);

        public virtual async Task<T> CreateAsync(T entity) => await _repository.CreateAsync(entity);

        public virtual async Task<T> UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

        public virtual async Task DeleteAsync(string id) => await _repository.DeleteAsync(id);
    }
}