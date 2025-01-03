using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Category>> GetMainCategories();
        Task<List<Category>> GetSubCategories();
    }
}
