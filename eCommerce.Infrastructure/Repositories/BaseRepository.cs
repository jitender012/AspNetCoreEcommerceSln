using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly eCommerceDbContext _context;
        protected readonly DbSet<T> _dbSet;
        //protected readonly ILogger _logger;
        public BaseRepository(eCommerceDbContext context/*, ILogger logger*/)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            //_logger = logger;
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = await _dbSet.Where(whereCondition).FirstOrDefaultAsync();
            return dbResult;
        }
        public async Task<T?> GetByIdAsync<Tid>(Tid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T?> GetByIdWithIncludeAsync<Tid>(Expression<Func<T, bool>> expression, Tid id)
        {
            return await _dbSet.Include(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await _dbSet.Where(whereCondition).ToListAsync();
        }

        public virtual async Task<T> InsertAsync(T entity)
        {

            var entry = await _dbSet.AddAsync(entity);  
            await _context.SaveChangesAsync(); 

            return entry.Entity; 

        }

        public virtual async Task  UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
           await _context.SaveChangesAsync();
        }

        public virtual Task UpdateAllAsync(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbSet.Attach(entity);
                 _context.Entry(entity).State = EntityState.Modified;
            }
             _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task  DeleteAsync(Expression<Func<T, bool>> whereCondition)
        {
            if (whereCondition == null)
            {
                throw new ArgumentNullException(nameof(whereCondition), "Condition for deletion cannot be null.");
            }

            try
            {
                var entity = await SingleOrDefaultAsync(whereCondition)
                    ?? throw new KeyNotFoundException($"Entity matching the condition was not found.");

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                //_logger?.LogInformation("Entity of type {EntityType} successfully deleted.", typeof(T).Name);
            }
            catch (DbUpdateException ex)
            {
                //_logger?.LogError(ex, "Database error while deleting entity of type {EntityType}.", typeof(T).Name);
                throw new ApplicationException("An error occurred while trying to delete the entity. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, "Unexpected error while deleting entity of type {EntityType}.", typeof(T).Name);
                throw new ApplicationException("An unexpected error occurred while deleting the entity.", ex);
            }
        }
        public Task<bool> ExistsAsync(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.AnyAsync(whereCondition);
        }
    }
}
