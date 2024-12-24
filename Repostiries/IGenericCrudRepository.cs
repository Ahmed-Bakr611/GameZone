
using Microsoft.EntityFrameworkCore;

namespace GameZone.Repostiries
{
    public interface IGenericCrudRepository<T> 
        where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task<bool> DeleteAsync(int id);
        public Task<int> SaveChangesAsync();
        public Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> query , bool asNoTracking = false);

    }
}