using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class ProductFeatureRepository : IProductFeatureRepository
    {
        private readonly eCommerceDbContext _context;
        private readonly ILogger<ProductFeatureRepository> _logger;
        public ProductFeatureRepository(eCommerceDbContext context, ILogger<ProductFeatureRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        #region Common Methods
        public async Task<int> InsertAsync(ProductFeature productFeature)
        {
            await _context.ProductFeatures.AddAsync(productFeature);
            await _context.SaveChangesAsync();

            return productFeature.ProductFeaturesId;
        }

        public async Task<IEnumerable<ProductFeature>> FetchAllAsync()
        {
            try
            {
                return await _context.ProductFeatures.ToListAsync();
            }
            catch (Exception)
            {
                _logger.LogError("Error occurred while fetching all Product Features.");
                throw;
            }
        }

        public async Task<ProductFeature> FetchByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid Product Feature ID: {id}", id);
                    throw new ArgumentException("Invalid product ID.");
                }

                _logger.LogInformation("Fetching  Feature Category with ID {ProductFeatureId}", id);

                var productFeature = await _context.ProductFeatures
                    .Where(x => x.ProductFeaturesId == id)
                    .Include(x => x.FeatureCategory)
                    .Include(x=>x.FeatureOptions)
                    .FirstOrDefaultAsync();

                if (productFeature == null)
                {
                    _logger.LogWarning("Product with ID {ProductFeatureId} not found.", id);
                    throw new KeyNotFoundException(" Feature Category not found.");
                }

                return productFeature;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Product Featurewith ID {ProductFeatureId}", id);
                throw;
            }

        }

        public async Task<bool> ModifyAsync(ProductFeature productFeature)
        {
            try
            {
                var existingRecord = await _context.ProductFeatures
               .Where(temp => temp.ProductFeaturesId == productFeature.ProductFeaturesId)
               .FirstOrDefaultAsync();

                if (existingRecord == null)
                {
                    throw new KeyNotFoundException("Record not found");
                }

                _context.Entry(existingRecord).CurrentValues.SetValues(productFeature);

                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Something went wrong while updating ProductFeature with ID {ProductFeaturesId}", productFeature.ProductFeaturesId);
                throw;
            }

        }

        public async Task<bool> RemoveAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid id: {Id}", id);
                return false;
            }

            try
            {
                var productFeature = await _context.ProductFeatures
               .FirstOrDefaultAsync(x => x.ProductFeaturesId == id);

                if (productFeature == null)
                {
                    _logger.LogInformation("Product Feature with id {Id} not found", id);
                    return false;
                }

                _context.ProductFeatures.Remove(productFeature);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Product Feature with id {Id} removed successfully", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing Product Feature with id {Id}", id);
                throw;
            }
        }

        #endregion


        public async Task<List<ProductFeature>> GetFeaturesByFeatureCategoryIdsAsync(List<int?> featureCategoryIds)
        {
            try
            {
                var productFeatures = await _context.ProductFeatures
                    .Where(f => featureCategoryIds.Contains(f.FeatureCategoryId))
                    .ToListAsync();

                return productFeatures;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching product features by feature category ids.");
                throw;
            }
        }

        //public async Task<List<ProductFeature>> GetProductFeaturesAsync(int featureCategoryId = 0, int productCategoryId = 0)
        //{
        //    try
        //    {
        //        IQueryable<ProductFeature> query = _context.ProductFeatures;

        //        if (featureCategoryId < 0 && productCategoryId < 0)
        //        {
        //            return query.ToList();
        //        }

        //        if (productCategoryId > 0 && featureCategoryId > 0)
        //        {
        //            query = query
        //                .Where(pf => _context.ProductCategoryFeatures
        //                    .Any(pcf => pcf.ProductFeaturesId == pf.ProductFeaturesId &&
        //                                pcf.FeatureCategoryId == featureCategoryId));
        //        }

        //        return await query.ToListAsync();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new Exception("Something went wrong.", ex);
        //    }
        //}

        public Task InsertMultipleProductFeatureAsync(IEnumerable<ProductFeature> productFeatures)
        {
            throw new NotImplementedException();
        }

        public Task<int> LinkToSpecificProductCategoryAsync(int featureId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<int> LinkProductFeatureToMultipleFeatureCategoriesAsync(int featureId, List<int> categoryIds)
        {
            throw new NotImplementedException();
        }

        public Task<int> LinkFeatureToFeatureCategoryAsync(int featureId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductFeature>> FetchByFeatureCategoryIdAsync(int featureCategoryId)
        {
            try
            {
                return await _context.ProductFeatures
                    .Where(x=>x.FeatureCategoryId == featureCategoryId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                _logger.LogError("Error occurred while fetching Product Features.");
                throw;
            }
        }




        //additional private methods


    }
}
