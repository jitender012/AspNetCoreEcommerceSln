using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface ICategoryRepository : IBaseRepository<ProductCategory>
    {
        Task<List<ProductCategory>> GetMainCategories();
        Task<List<ProductCategory>> GetSubCategories();
        Task<List<ProductCategory>> GetChildCategoriesAsync();
        Task<List<ProductCategory>> GetHierarchicalCategories();
        Task<ProductCategory?> GetCategoryByIdAsync(int categoryId);
        Task<List<ProductCategory>> FetchByFeaureCategoryIdAsync(int featureCategoryId);

        /// <summary>
        /// Get Product Categories that are not linked with a particular Feature Category
        /// </summary>
        /// <param name="featureCategoryId">Feature Category Id</param>
        /// <returns>List of Product Categories</returns>
        Task<List<ProductCategory>> FetchUnlinkedProductCategories(int featureCategoryId);
    }
}
