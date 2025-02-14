using eCommerce.Application.DTO;
using eCommerce.Application.DTO.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts.ProductServiceContracts
{
    public interface IFeatureCategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all Feature categories</returns>
        Task<List<FeatureCategoryDTO>> GetAllAsync();

        Task<FeatureCategoryDTO> GetByIdAsync(int id);

        /// <summary>
        /// Creates new FeatureCategory
        /// </summary>
        /// <param name="data">Expects ProductFeatureDTO object</param>
        /// <returns>Id of created FeatureCategory</returns>
        Task<int> AddAsync(FeatureCategoryDTO data);

        Task<bool> UpdateAsync(FeatureCategoryDTO data);

        Task<bool> DeleteAsync(int id);
    }
}
