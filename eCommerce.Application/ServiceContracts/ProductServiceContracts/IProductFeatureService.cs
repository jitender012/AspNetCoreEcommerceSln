using eCommerce.Application.DTO.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts.ProductServiceContracts
{
    public interface IProductFeatureService
    {
        #region Common Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all Feature categories</returns>
        Task<List<ProductFeatureDTO>> GetAllAsync();

        Task<ProductFeatureDTO> GetByIdAsync(int id);

        /// <summary>
        /// Creates new Product Feature
        /// </summary>
        /// <param name="data">Expects Product Feature object</param>
        /// <returns>Id of created Product Feature</returns>
        Task<int> AddAsync(ProductFeatureDTO data);

        Task<bool> UpdateAsync(ProductFeatureDTO data);

        Task<bool> DeleteAsync(int id);
        #endregion
    }
}
