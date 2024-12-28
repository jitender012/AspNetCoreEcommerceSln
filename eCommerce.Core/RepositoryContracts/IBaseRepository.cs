using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Retrieve a single item by it's primary key or return null if not found
        /// </summary>
        /// <param name="primaryKey">Prmary key to find</param>
        /// <returns>T</returns>
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition);
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Returns all the rows for type T
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Returns all the rows for type T on basis of filter condition
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Inserts the data into the table
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Updates this entity in the database using it's primary key
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="userId">The user performing the update</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Updates all the passed entities in the database 
        /// </summary>
        /// <param name="entities">Entities to update</param>
        Task UpdateAllAsync(IList<T> entities);

        /// <summary>
        /// Deletes this entry fro the database
        /// ** WARNING - Most items should be marked inactive and Updated, not deleted
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="userId">The user Id who deleted the entity</param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Does this item exist by it's primary key
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> whereCondition);
    }
}
