using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Domain.CustomException;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Services.ProductServices
{
    public class ProductFeatureService(IProductFeatureRepository productFeatureRepository, IUserContextService userContextService, ILogger<ProductFeatureService> logger) : IProductFeatureService
    {
        private readonly IProductFeatureRepository _productFeatureRepository = productFeatureRepository;
        private readonly IUserContextService _userContextService = userContextService;
        private readonly ILogger<ProductFeatureService> _logger = logger;


        public async Task<List<ProductFeatureDTO>> GetAllAsync()
        {
            var productFeatures = await _productFeatureRepository.FetchAllAsync();

            if (productFeatures == null || !productFeatures.Any())
            {
                _logger.LogWarning("No product features found.");
                return [];
            }

            var productFeatureDTOs = productFeatures.Select(pf => new ProductFeatureDTO
            {
                Name = pf.Name,
                CreatedBy = pf.CreatedBy,
                ProductFeatureId = pf.ProductFeaturesId,
                IsManadatory = pf.IsManadatory
            }).ToList();

            return productFeatureDTOs;
        }

        public async Task<ProductFeatureDTO> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid id.");
                throw new ArgumentNullException("ID must be greater than zero");
            }
            var productFeature = await _productFeatureRepository.FetchByIdAsync(id);

            if (productFeature == null)
            {
                _logger.LogWarning("Product feature not found with ID: {Id}", id);
                return new ProductFeatureDTO();
            }

            var pf = new ProductFeatureDTO
            {
                ProductFeatureId = productFeature.ProductFeaturesId,
                Name = productFeature.Name,
                CreatedBy = productFeature.CreatedBy,
                IsManadatory = productFeature.IsManadatory,
                InputType = productFeature.InputType,
                FeatureCategoryId = productFeature.FeatureCategoryId!.Value,
                

                FeatureOptions = productFeature.FeatureOptions.Select(fo => new FeatureOptionDTO
                {
                    FeatureOptionId = fo.FeatureOptionId,
                    Value = fo.Value,
                    CreatedBy = fo.CreatedBy
                }).ToList()
            };
            if (productFeature.FeatureCategory !=null)
            {
                pf.FeatureCategoryName = productFeature.FeatureCategory.Name;
            }

            return pf;

        }

        public async Task<int> AddAsync(ProductFeatureDTO data)
        {
            if (data == null)
            {
                _logger.LogError("ProductFeatureDTO is null.");
                throw new ArgumentNullException(nameof(data), "Product feature data is required.");
            }

            if (string.IsNullOrWhiteSpace(data.Name))
            {
                _logger.LogError("Attempted to create a Product Feature with an empty name.");
                throw new ArgumentException("Feature name is required.");
            }

            var userId = _userContextService.GetUserId();

            ProductFeature pf = new()
            {
                Name = data.Name,
                IsManadatory = data.IsManadatory,
                CreatedBy = userId.ToString(),
                InputType = data.InputType,
                FeatureCategoryId = data.FeatureCategoryId
            };

            try
            {
                var id = await _productFeatureRepository.InsertAsync(pf);
                return id;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error while inserting product feature.");
                throw;
            }
        }


        public async Task<bool> UpdateAsync(ProductFeatureDTO data)
        {
            if (data == null || data.ProductFeatureId <= 0)
            {
                _logger.LogError("Invalid ProductFeature data for update.");
                throw new ArgumentException("Invalid product feature data.");
            }

            var existingFeature = await _productFeatureRepository.FetchByIdAsync(data.ProductFeatureId);
            if (existingFeature == null)
            {
                _logger.LogWarning("Product feature not found with ID: {Id}", data.ProductFeatureId);
                return false;
            }

            existingFeature.Name = data.Name;
            existingFeature.IsManadatory = data.IsManadatory;
            existingFeature.FeatureCategoryId = data.FeatureCategoryId;
            existingFeature.InputType = data.InputType;

            await _productFeatureRepository.ModifyAsync(existingFeature);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid ProductFeature ID: {Id}", id);
                throw new ArgumentException("ID must be greater than zero.");
            }

            var data = await _productFeatureRepository.FetchByIdAsync(id);
            if (data == null)
            {
                _logger.LogWarning("No product feature found with ID: {Id}", id);
                return false;
            }

            await _productFeatureRepository.RemoveAsync(id);
            return true;
        }

        public async Task<List<ProductFeatureDTO>> GetByFeatureCategoryIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid ProductFeature ID: {Id}", id);
                throw new ArgumentException("ID must be greater than zero.");
            }

            var data = await _productFeatureRepository.FetchByFeatureCategoryIdAsync(id);

            if (data == null)
            {
                _logger.LogWarning("No product feature found with ID: {Id}", id);
                return [];
            }

            var productFeatures = data.Select(x => new ProductFeatureDTO()
            {
                ProductFeatureId  = x.ProductFeaturesId,
                Name = x.Name,
                InputType = x.InputType,
                IsManadatory = x.IsManadatory
            }).ToList();
            
            return productFeatures;
        }
    }
}
