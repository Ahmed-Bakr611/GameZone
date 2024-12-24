
namespace GameZone.Repostiries
{
    public class GenericCrudRepository<T> : IGenericCrudRepository<T>
     where T : class
    {


        private readonly GameDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericCrudRepository(GameDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
             _dbSet.Update(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            
              _dbSet.Remove(entity);
               return true;
           
        }

        public async Task<int> SaveChangesAsync()
        {
           return  await _context.SaveChangesAsync();
        }
    

        public async Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(
         Func<IQueryable<T>, IQueryable<TResult>> query, bool asNoTracking = true)
        {
            IQueryable<T> baseQuery = _dbSet;

            // Apply AsNoTracking if specified
            if (asNoTracking)
            {
                baseQuery = baseQuery.AsNoTracking();
            }

            var resultQuery = query(baseQuery);
            return await resultQuery.ToListAsync();
        }


    }
}
