using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface IBrandRepository  
    {
    
        Task<Brand?> GetBrandById(Guid id);
        Task<List<Brand>> GetAllBrands();
        Task<Guid> InsertBrandAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task UpdateStatusAsync(Guid brandId);
        Task<bool> SoftDeleteAsync(Guid brandId);
        Task<bool> ExistsByNameAsync(string brandName);
    }
}
