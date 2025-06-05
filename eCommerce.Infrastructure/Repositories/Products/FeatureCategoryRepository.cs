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

        #region Basic CRUD implementations
        public async Task<bool> ModifyAsync(FeatureCategory featureCategory)
        {
            try
            {
                var existingRecord = await _context.FeatureCategories
                              .Where(temp => temp.FeatureCategoryId == featureCategory.FeatureCategoryId)
                              .FirstOrDefaultAsync();

                if (existingRecord == null)
                {
                    throw new KeyNotFoundException("Record not found");
                }

                _context.Entry(existingRecord).CurrentValues.SetValues(featureCategory);

                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Something went wrong while updating FeatureCategory with ID {FeatureCategoryId}", featureCategory.FeatureCategoryId);
                throw;
            }
        }
        #endregion

        #region InsertOperations
        public async Task<int> InsertAsync(FeatureCategory featureCategory, int productCategoryId)
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
        public Task<int> LinkToProductCategoryAsync(int featureId, int featureCategoryIds)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UnlinkCategoryFeature(int productCategoryId, int featureCategoryId)
        {
            var record = await _context.ProductCategoryFeatures
                            .Where(pcf => pcf.ProductCategoryId == productCategoryId && pcf.FeatureCategoryId == featureCategoryId)
                            .FirstOrDefaultAsync();
            if (record == null)
            {
                return false;
            }
            _context.ProductCategoryFeatures.Remove(record);
            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

        #region ReadOperations
        public async Task<IEnumerable<FeatureCategory>> FetchAllAsync()
        {
            try
            {
                return await _context.FeatureCategories
                    .Where(x => x.IsDeleted == false)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when fetching information.", ex);
            }
        }

        public async Task<IEnumerable<FeatureCategory>> FetchByProductCategoryIdAsync(int productCategoryId)
        {
            try
            {
                List<FeatureCategory> featureCategories = await _context.ProductCategoryFeatures
                                       .Where(pfc => pfc.ProductCategoryId == productCategoryId)
                                       .Include(f => f.FeatureCategory)
                                           .ThenInclude(f => f.ProductFeatures)
                                       .Select(f => f.FeatureCategory)
                                       .ToListAsync();

                //List<ProductFeature> productFeatures = await _context.ProductFeatures.Where(pf=>pf.ProductFeaturesId == featureCategories)
                return featureCategories;
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


        public async Task<FeatureCategory> FindDetailsAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid Feature Category ID: {FeatureCategoryId}", id);
                    throw new ArgumentException("Invalid product ID.");
                }

                _logger.LogInformation("Fetching  Feature Category with ID {FeatureCategoryId}", id);
                var FeatureCategory = await _context
                    .FeatureCategories
                    .Include(x => x.ProductFeatures)
                    .SingleOrDefaultAsync(x => x.FeatureCategoryId == id);

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
            if (id <= 0)
            {
                _logger.LogWarning("Invalid id");
                return false;
            }

            var featureCategory = await _context.FeatureCategories
                .FirstOrDefaultAsync(x => x.FeatureCategoryId == id);

            if (featureCategory == null)
            {
                _logger.LogInformation("Feature category not found");
                return false;
            }

            featureCategory.IsDeleted = true;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Feature category removed successfully. Id: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting feature category. Id: {Id}", id);
                return false;
            }
        }

        public async Task<bool> LinkFeatCatToProdCat(int featureCategoryId, int productCategoryId)
        {
            try
            {
                var pcf = new ProductCategoryFeature()
                {
                    FeatureCategoryId = featureCategoryId,
                    ProductCategoryId = productCategoryId
                };
                await _context.AddAsync(pcf);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
