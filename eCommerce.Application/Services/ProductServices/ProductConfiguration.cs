using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Services
{
    public class ProductConfiguration(IFeatureOptionRepository featureOptionRepository, IProductFeatureRepository productFeatureRepository, IFeatureCategoryRepository featureCategoryRepository, IUserContextService userContext, ILogger<ProductConfiguration> logger) : IProductConfiguration
    {
        private readonly IFeatureOptionRepository _featureOptionsRepository = featureOptionRepository;
        private readonly IProductFeatureRepository _productFeatureRepository = productFeatureRepository;
        private readonly IFeatureCategoryRepository _featureCategoryRepository = featureCategoryRepository;
        private readonly IUserContextService _userContextService = userContext;
        ILogger<ProductConfiguration> _logger = logger;


        #region ProductFeatureMethods
        public async Task AddProductFeatureAsync(ProductFeatureDTO productFeature)
        {
            try
            {
                // Validate input
                if (productFeature == null)
                    throw new ArgumentNullException(nameof(productFeature), "Product feature data is null.");

                if (string.IsNullOrWhiteSpace(productFeature.Name))
                    throw new ArgumentException("Product feature name is required.");

                var userId = _userContextService.GetUserId()?.ToString();

                ProductFeature pf = new()
                {
                    Name = productFeature.Name,
                    IsManadatory = false,
                    CreatedBy = userId!
                };
                
                await _productFeatureRepository.InsertAsync(pf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a product feature.");

                throw;
            }
        }

        public Task AddMultipleProductFeatureAsync(IEnumerable<int> FeatureIds)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region FeatureOptionsMethods
        public async Task AddFeatureOptionsAsync(FeatureOptionDTO featureOption)
        {
            FeatureOption fo = new()
            {
                ProductFeatureId = featureOption.ProductFeatureId,
                Value = featureOption.Value
            };
            await _featureOptionsRepository.InsertAsync(fo);
        }

        #endregion


        #region FeatureCategoryMethods
        public Task AddFeatureCategoryAsync(FeatureCategory category)
        {
            throw new NotImplementedException();
        }
        public Task<List<FeatureOption>> GetFeatureOptionsByProductFeatureAsync(int productFeatureId)
        {
            throw new NotImplementedException();
        }
        public Task<List<ProductFeature>> GetProductFeaturesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<int> LinkFeatureToCategoryAsync(int featureId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<int> LinkFeatureToFeatureCategoryAsync(int featureId, int categoryId)
        {
            throw new NotImplementedException();
        }
        public Task<int> LinkFeatureToMultipleCategoriesAsync(int featureId, List<int> categoryIds)
        {
            throw new NotImplementedException();
        }

        public Task<int> LinkFeatureToMultipleFeatureCategoriesAsync(int featureId, int featureCategoryIds)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
