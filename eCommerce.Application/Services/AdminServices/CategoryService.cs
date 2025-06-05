using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;

namespace eCommerce.Application.Services.AdminServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserContextService _userContext;
        public CategoryService(ICategoryRepository categoryRepository, IUserContextService userContextService)
        {
            _categoryRepository = categoryRepository;
            _userContext = userContextService;
        }
        public async Task<int> AddCategoryAsync(CategoryDTO data)
        {
            if (data.CategoryName == null)
                throw new ArgumentException("Category name is required.");

            ProductCategory category = new ProductCategory()
            {
                CategoryName = data.CategoryName,
                CategoryImage = data.CategoryImage,
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            var userId = _userContext.GetUserId();

            if (userId.HasValue)
            {
                category.CreatedBy = userId.Value;
            }

            if (data.ParentCategoryId > 0)
            {
                category.ParentCategoryId = data.ParentCategoryId;
            }

            var result = await _categoryRepository.InsertAsync(category);

            return result.ProductCategoryId;
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
            await _categoryRepository.DeleteAsync(x => x.ProductCategoryId == id);
            return true;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoryDTO = categories.Select(x => new CategoryDTO()
            {
                CategoryId = x.ProductCategoryId,
                CategoryImage = x.CategoryImage,
                ParentCategoryId = x.ParentCategoryId,
                CategoryName = x.CategoryName,
                //categoryDTO = x.ParentCategoryId > 0 ? categories.Select(y=> new CategoryDTO
                //{
                //    CategoryId = x.CategoryId,
                //    CategoryImage = x.CategoryImage,
                //    ParentCategoryId = x.ParentCategoryId,
                //    CategoryName = x.CategoryName,
                //}).FirstOrDefault() : null
            }).ToList();

            return categoryDTO;
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            if (id < 1)
                throw new ArgumentNullException("Invalid Brand Id.");

            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Brand with ID {id} not found.");
            }

            var categoryDTO = new CategoryDTO()
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
            };
            if (category.ParentCategoryId > 0)
            {
                categoryDTO.ParentCategoryId = category.ParentCategoryId;
                categoryDTO.ParentCategoryName = category.ParentCategory!.CategoryName;
            }

            return categoryDTO;
        }

        public async Task<List<CategoryDTO>> GetMainCategoriesAsync()
        {
            var categories = await _categoryRepository.GetMainCategories();

            var catDTO = categories.Select(x => new CategoryDTO
            {
                CategoryId = x.ProductCategoryId,
                CategoryImage = x.CategoryImage,
                CategoryName = x.CategoryName,
                ParentCategoryId = x.ParentCategoryId
            }).ToList();
            return catDTO;
        }

        public async Task<List<CategoryDTO>> GetByFeatureCategoryIdAsync(int featureCategory)
        {
            var categories = await _categoryRepository.FetchByFeaureCategoryIdAsync(featureCategory);

            var catDTO = categories.Select(x => new CategoryDTO
            {
                CategoryId = x.ProductCategoryId,
                CategoryImage = x.CategoryImage,
                CategoryName = x.CategoryName,
                ParentCategoryId = x.ParentCategoryId
            }).ToList();
            return catDTO;
        }
        public async Task<List<CategoryDTO>> GetSubCategoriesAsync()
        {
            var categories = await _categoryRepository.GetSubCategories();
            return CategoryDTO.FromCategoryList(categories);
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesHierarchicalAsync()
        {
            var categories = await _categoryRepository.GetHierarchicalCategories();
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

            category.ProductCategoryId = data.CategoryId;
            category.CategoryImage = data.CategoryImage;
            category.CategoryName = data.CategoryName;
            category.ParentCategoryId = data.ParentCategoryId;

            await _categoryRepository.UpdateAsync(category);

            return true;
        }

        public async Task<List<CategoryDTO>> GetChildCategoriesAsync()
        {
            var category = await _categoryRepository.GetChildCategoriesAsync();

            var categoryDTO = category.Select(category => new CategoryDTO
            {
                CategoryId = category.ProductCategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
            }).ToList();

            return categoryDTO;
        }

        public async Task<List<CategoryDTO>> GetUnlinkedProductCategories(int featureCategoryId)
        {
            var categories = await _categoryRepository.FetchUnlinkedProductCategories(featureCategoryId);
            var categoriesDTO = CategoryDTO.FromCategoryList(categories);

            return categoriesDTO;
        }
    }
}
