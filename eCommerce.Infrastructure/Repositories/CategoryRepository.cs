using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<ProductCategory>, ICategoryRepository
    {
        private readonly new eCommerceDbContext _context;
        public CategoryRepository(eCommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> GetMainCategories()
        {
            return await _context.ProductCategories.Where(x => x.ParentCategoryId == null).ToListAsync();
        }

        public async Task<List<ProductCategory>> GetSubCategories()
        {
            return await _context.ProductCategories.Where(x => x.ParentCategoryId > 0).ToListAsync();
        }
        public async Task<List<ProductCategory>> GetHierarchicalCategories()
        {
            var allCategories = await _context.ProductCategories
                             .Include(c => c.InverseParentCategory)
                             .ToListAsync();

            // Usage
            var hierarchicalCategories = BuildCategoryHierarchy(allCategories);
            return hierarchicalCategories;
        }
        List<ProductCategory> BuildCategoryHierarchy(List<ProductCategory> categories, int? parentId = null)
        {
            return categories
                .Where(c => c.ParentCategoryId == parentId)
                .Select(c => new ProductCategory
                {
                    ProductCategoryId = c.ProductCategoryId,
                    CategoryName = c.CategoryName,
                    ParentCategoryId = c.ParentCategoryId,
                    InverseParentCategory = BuildCategoryHierarchy(categories, c.ProductCategoryId)
                })
                .ToList();
        }

        public async Task<ProductCategory?> GetCategoryByIdAsync(int categoryId)
        {
            ProductCategory? category = await _context
                .ProductCategories
                .Include(x=>x.ParentCategory)
                .FirstOrDefaultAsync(x=>x.ProductCategoryId == categoryId);
            return category;
        }
    }
}
