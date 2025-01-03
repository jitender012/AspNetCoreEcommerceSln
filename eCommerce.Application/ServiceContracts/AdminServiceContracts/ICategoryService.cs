using eCommerce.Application.DTO;
using eCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts.AdminServiceContracts
{
    public interface ICategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of categories</returns>
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<List<CategoryDTO>> GetMainCategoriesAsync();
        Task<List<CategoryDTO>> GetSubCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Creates new category
        /// </summary>
        /// <param name="data">Expects CategoryDTO object</param>
        /// <returns>Id of created category</returns>
        Task<int> AddCategoryAsync(CategoryDTO data);
        Task<bool> UpdateCategoryAsync(CategoryDTO data);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
