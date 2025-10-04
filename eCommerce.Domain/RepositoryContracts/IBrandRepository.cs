using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface IBrandRepository  : IBaseRepository<Brand>
    {
        /// <summary>
        /// Retrieves a Brand entity by its unique identifier.  
        /// </summary>
        /// <param name="id">The unique identifier of the Brand.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the Brand entity if found; otherwise, null.
        /// </returns>
        //Task<Brand?> GetBrandById(Guid id);
        Task<List<Brand>> GetAllBrands();
        //Task<Guid> CreateAsync(Brand brand);
        //Task UpdateAsync(Brand brand);
        Task<bool> SoftDeleteAsync(Guid brandId);
        Task<bool> ExistsByNameAsync(string brandName);
    }
}
