using eCommerce.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts.AdminServiceContracts
{
    public interface IProductCategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of categories</returns>
        Task<List<CategoryDTO>> GetAllAsync();
        Task<List<CategoryDTO>> GetMainCategoriesAsync();
        Task<List<CategoryDTO>> GetSubCategoriesAsync();
        Task<List<CategoryDTO>> GetChildCategoriesAsync();
        Task<List<CategoryDTO>> GetAllCategoriesHierarchicalAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        //Task<List<CategoryDTO>> GetByFeatureCategoryIdAsync(int id);


        /// <summary>
        /// Creates new category
        /// </summary>
        /// <param name="data">Expects CategoryDTO object</param>
        /// <returns>Id of created category</returns>
        Task<int> AddCategoryAsync(CategoryDTO data);
        Task<bool> UpdateCategoryAsync(CategoryDTO data);
        Task<bool> DeleteCategoryAsync(int id);
        //Task<List<CategoryDTO>> GetUnlinkedProductCategories(int featureCategoryId);

    }
}
