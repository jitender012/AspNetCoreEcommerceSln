using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly eCommerceDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(eCommerceDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = await _dbSet.Where(whereCondition).FirstOrDefaultAsync();
            return dbResult;
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
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
            var entity = await SingleOrDefaultAsync(whereCondition);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with ID {whereCondition} not found.");

            _dbSet.Remove(entity);
        }
        public Task<bool> ExistsAsync(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.AnyAsync(whereCondition);
        }
    }
}
