using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Core.DTO;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;

namespace eCommerce.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<int> AddCategoryAsync(CategoryDTO data)
        {
            if (data.CategoryName == null)
                throw new ArgumentException("Category name is required.");

            Category category = new Category()
            {
                CategoryName = data.CategoryName,
                CategoryImage = data.CategoryImage
            };
            if (data.ParentCategoryId > 0)
            {
                category.ParentCategoryId = data.ParentCategoryId;
            }

            var result = await _categoryRepository.InsertAsync(category);

            return result.CategoryId;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id < 1)
                throw new ArgumentNullException("Invalid Category Id.");

            var category = await _categoryRepository
                            .GetByIdAsync(id);

            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }
            await _categoryRepository.DeleteAsync(x => x.CategoryId == id);
            return true;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoryDTO = categories.Select(x => new CategoryDTO()
            {
                CategoryId = x.CategoryId,
                CategoryImage = x.CategoryImage,
                ParentCategoryId = x.ParentCategoryId,
                CategoryName = x.CategoryName
            }).ToList();

            return categoryDTO;
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            if (id < 1)
                throw new ArgumentNullException("Invalid Brand Id.");

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Brand with ID {id} not found.");
            }

            var categoryDTO = new CategoryDTO()
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
                ParentCategoryId = category.ParentCategoryId
            };

            return categoryDTO;
        }

        public async Task<List<CategoryDTO>> GetMainCategoriesAsync()
        {
            var categories = await _categoryRepository.GetMainCategories();
            return CategoryDTO.FromCategoryList(categories);
        }

        public async Task<List<CategoryDTO>> GetSubCategoriesAsync()
        {
            var categories = await _categoryRepository.GetSubCategories();
            return CategoryDTO.FromCategoryList(categories);
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDTO data)
        {
            // Fetch the existing brand from the database
            var category = await _categoryRepository.GetByIdAsync(data.CategoryId);

            if (category == null)
            {
                return false;
            }

            category.CategoryId = data.CategoryId;
            category.CategoryImage = data.CategoryImage;
            category.CategoryName = data.CategoryName;
            category.ParentCategoryId = data.ParentCategoryId;

            await _categoryRepository.UpdateAsync(category);

            return true;
        }
    }
}
