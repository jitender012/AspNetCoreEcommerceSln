using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class FeatureCategoryRepository : IFeatureCategoryRepository
    {
        private readonly eCommerceDbContext _context;
        private readonly ILogger<FeatureCategoryRepository> _logger;
        public FeatureCategoryRepository(eCommerceDbContext context, ILogger<FeatureCategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region InsertOperations
        public async Task<int> InsertFeatureCategoryAsync(FeatureCategory featureCategory, int productCategoryId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.FeatureCategories.AddAsync(featureCategory);
                await _context.SaveChangesAsync();

                ProductCategoryFeature pcf = new()
                {
                    FeatureCategoryId = featureCategory.FeatureCategoryId,
                    ProductCategoryId = productCategoryId,
                };

                await _context.ProductCategoryFeatures.AddAsync(pcf);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return pcf.FeatureCategoryId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Something went wrong.", ex);
            }
        }
        public async Task InsertMultipleProductFeatureCategoryAsync(IEnumerable<FeatureCategory> categories)
        {
            try
            {
                await _context.FeatureCategories.AddRangeAsync(categories);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while inserting feature category.", ex);
            }
        }
        public Task<int> LinkFeatureToMultipleFeatureCategoriesAsync(int featureId, int featureCategoryIds)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ReadOperations
        public async Task<IEnumerable<FeatureCategory>> FetchAllAsync(int productCategoryId = 0)
        {
            try
            {
                if (productCategoryId > 0)
                {
                    return await _context.FeatureCategories
                                         .Where(fc => fc.FeatureCategoryId == productCategoryId)
                                         .ToListAsync();
                }

                return await _context.FeatureCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when fetching information.", ex);
            }
        }

        public async Task<FeatureCategory> FindByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid Feature Category ID: {FeatureCategoryId}", id);
                    throw new ArgumentException("Invalid product ID.");
                }

                _logger.LogInformation("Fetching  Feature Category with ID {FeatureCategoryId}", id);
                var FeatureCategory = await _context.FeatureCategories.FindAsync(id);

                if (FeatureCategory == null)
                {
                    _logger.LogWarning("Product with ID {FeatureCategoryId} not found.", id);
                    throw new KeyNotFoundException(" Feature Category not found.");
                }

                return FeatureCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching  Feature Category with ID {FeatureCategoryId}", id);
                throw;              
            }
        }
        #endregion

        public async Task<bool> RemoveAsync(int id)
        {
            if (id<=0)
            {
                _logger.LogWarning("Invalid id");
                return false;
            }
            var FeatureCategory = await _context.FeatureCategories.Where(x => x.FeatureCategoryId == id).FirstOrDefaultAsync();

            if (FeatureCategory == null) 
            {
                _logger.LogInformation("Feature category not found");
                return false;
            }
            _context.FeatureCategories.Remove(FeatureCategory);
            _context.SaveChanges();

            _logger.LogInformation("Feature category removed successfully");
            return true;
        }
    }
}
