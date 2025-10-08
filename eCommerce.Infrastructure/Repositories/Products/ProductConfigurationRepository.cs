using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class ProductConfigurationRepository : IProductConfigurationRepository
    {
        private readonly eCommerceDbContext _context;
        public ProductConfigurationRepository(eCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> LinkFeatureToCategoryAsync(List<ProductCategoryProductFeature> productCategoryProductFeatures)
        {
            await _context.ProductCategoryProductFeature.AddRangeAsync(productCategoryProductFeatures);
            try
            {
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> UnlinkFeatureFromCategoryAsync(List<ProductCategoryProductFeature> productCategoryProductFeatures)
        {
            try
            {
                // Get all pairs (categoryId, featureId) to check
                var categoryIds = productCategoryProductFeatures.Select(x => x.ProductCategoryId).ToList();
                var featureIds = productCategoryProductFeatures.Select(x => x.ProductFeatureId).ToList();

                // Fetch existing records from DB
                var existingLinks = await _context.ProductCategoryProductFeature
                    .Where(x => categoryIds.Contains(x.ProductCategoryId) &&
                                featureIds.Contains(x.ProductFeatureId))
                    .ToListAsync();

                if (existingLinks.Any())
                {
                    _context.ProductCategoryProductFeature.RemoveRange(existingLinks);
                    var result = await _context.SaveChangesAsync();
                    return result > 0;
                }

                return false; // nothing existed to remove
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
