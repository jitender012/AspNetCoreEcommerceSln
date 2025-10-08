using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        private readonly new eCommerceDbContext _context;
        public ProductCategoryRepository(eCommerceDbContext context) : base(context) => _context = context;

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
        private List<ProductCategory> BuildCategoryHierarchy(List<ProductCategory> categories, int? parentId = null)
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
                .Include(x => x.ParentCategory)
                .FirstOrDefaultAsync(x => x.ProductCategoryId == categoryId);
            return category;
        }

        public async Task<List<ProductCategory>> GetChildCategoriesAsync()
        {
            var childCategories = await _context
                .ProductCategories
                .Where(c => !_context.ProductCategories
                            .Any(sub => sub.ParentCategoryId == c.ProductCategoryId))
                .ToListAsync();

            return childCategories;
        }

        //public async Task<List<ProductCategory>> FetchByFeaureCategoryIdAsync(int featureCategoryId)
        //{
        //    var categories = await _context
        //       .ProductCategoryFeatures
        //       .Where(c => c.FeatureCategoryId == featureCategoryId)
        //       .Select(c => c.ProductCategory)
        //       .ToListAsync();

        //    return categories;
        //}

        //public async Task<List<ProductCategory>> FetchUnlinkedProductCategories(int featureCategoryId)
        //{
        //    // Get all category IDs that are already linked
        //    var linkedCategoryIds = await _context.ProductCategoryFeatures
        //        .Where(pcf => pcf.FeatureCategoryId == featureCategoryId)
        //        .Select(pcf => pcf.ProductCategoryId)
        //        .ToListAsync();

        //    // Get leaf categories (those not acting as a ParentCategory)
        //    var leafCategories = await _context.ProductCategories
        //        .Where(pc => !_context.ProductCategories.Any(child => child.ParentCategoryId == pc.ProductCategoryId))
        //        .Where(pc => !linkedCategoryIds.Contains(pc.ProductCategoryId))
        //        .ToListAsync();

        //    return leafCategories;
        //}

        public async Task<List<ProductCategory>> GetAllCategories()
        {
            var categories = await _context.ProductCategories
                   .Include(c => c.ParentCategory)
                   .ToListAsync();
            return categories;
        }

        public async Task<List<int>> GetAllDescendantsIds(int categoryId)
        {
            var allIds = new List<int>();
            await AddDescendants(categoryId, allIds);
            return allIds;
        }

        private async Task AddDescendants(int parentId, List<int> allIds)
        {
            var childCategories = await _context.ProductCategories
                .Where(c => c.ParentCategoryId == parentId)
                .Select(c => c.ProductCategoryId)
                .ToListAsync();

            if (!childCategories.Any())
                return;

            allIds.AddRange(childCategories);

            foreach (var childId in childCategories)
            {
                await AddDescendants(childId, allIds);
            }
        }
    }
}
