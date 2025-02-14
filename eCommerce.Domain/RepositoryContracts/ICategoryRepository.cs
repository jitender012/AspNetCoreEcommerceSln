using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface ICategoryRepository : IBaseRepository<ProductCategory>
    {
        Task<List<ProductCategory>> GetMainCategories();
        Task<List<ProductCategory>> GetSubCategories();
        Task<List<ProductCategory>> GetHierarchicalCategories();
        Task<ProductCategory?> GetCategoryByIdAsync(int categoryId);
    }
}
