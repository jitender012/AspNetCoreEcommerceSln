using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
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
                ProductFeaturesId = pf.ProductFeaturesId,
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
            var productFeature =await _productFeatureRepository.FetchByIdAsync(id);

            if (productFeature == null)
            {
                _logger.LogWarning("Product feature not found with ID: {Id}", id);
                return new ProductFeatureDTO(); 
            }

            return new ProductFeatureDTO()
            {
                ProductFeaturesId = productFeature.ProductFeaturesId,
                Name = productFeature.Name,
                CreatedBy = productFeature.CreatedBy,
                IsManadatory = productFeature.IsManadatory
            };          
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

            string userId = _userContextService.GetUserId()?.ToString() ?? "000000000";

            ProductFeature pf = new()
            {
                Name = data.Name,
                CreatedBy = userId,
                IsManadatory = data.IsManadatory
            };
            try
            {
                var id = await _productFeatureRepository.InsertAsync(pf, data.FeatureCategoryId);
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
            if (data == null || data.ProductFeaturesId <= 0)
            {
                _logger.LogError("Invalid ProductFeature data for update.");
                throw new ArgumentException("Invalid product feature data.");
            }

            var existingFeature = await _productFeatureRepository.FetchByIdAsync(data.ProductFeaturesId);
            if (existingFeature == null)
            {
                _logger.LogWarning("Product feature not found with ID: {Id}", data.ProductFeaturesId);
                return false; 
            }

            existingFeature.Name = data.Name;
            existingFeature.IsManadatory = data.IsManadatory;            

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
    }
}
