using eCommerce.Application.DTO;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.ProductServices
{
    public class FeatureCategoryService(IFeatureCategoryRepository featureCategoryRepository, IUserContextService userContextService, ILogger<FeatureCategoryService> logger) : IFeatureCategoryService
    {
        private readonly IFeatureCategoryRepository _featureCategoryRepository = featureCategoryRepository;
        private readonly IUserContextService _userContextService = userContextService;
        private readonly ILogger<FeatureCategoryService> _logger = logger;

        public async Task<List<FeatureCategoryDTO>> GetAllAsync()
        {
            IEnumerable<FeatureCategory> featureCategories = await _featureCategoryRepository.FetchAllAsync();

            List<FeatureCategoryDTO> featureCategoryDTOs = featureCategories.Select(temp => new FeatureCategoryDTO
            {
                Name = temp.Name,
                FeatureCategoryId = temp.FeatureCategoryId,
                CreatedBy = temp.CreatedBy,
                IsMandatory = temp.IsMandatory
            }).ToList();

            _logger.LogInformation("");
            return featureCategoryDTOs;
        }
        public async Task<List<FeatureCategoryDTO>> GetByProductCategoryIdAsync(int id)
        {
            IEnumerable<FeatureCategory> featureCategories = await _featureCategoryRepository.FetchByProductCategoryIdAsync(id);
            List<FeatureCategoryDTO> featureCategoryDTOs = featureCategories.Select(temp => new FeatureCategoryDTO
            {
                Name = temp.Name,
                FeatureCategoryId = temp.FeatureCategoryId,
                CreatedBy = temp.CreatedBy,
                IsMandatory = temp.IsMandatory,
                ProductFeatures = temp.ProductFeatures
                                        .Select(x => new ProductFeatureDTO
                                        {
                                            Name = x.Name,
                                            CreatedBy = x.CreatedBy,
                                            ProductFeatureId = x.ProductFeaturesId,
                                            IsManadatory = x.IsManadatory,
                                        }).ToList()
            }).ToList();

            _logger.LogInformation("");
            return featureCategoryDTOs;
        }
        public async Task<FeatureCategoryDTO> GetByIdAsync(int id)
        {
            var featureCategory = await _featureCategoryRepository.FindByIdAsync(id);

            FeatureCategoryDTO featureCategoryDTO = new()
            {
                FeatureCategoryId =featureCategory.FeatureCategoryId,
                CreatedBy = featureCategory.CreatedBy,
                DisplayOrder = featureCategory.DisplayOrder,
                IsMandatory = featureCategory.IsMandatory,
                Name = featureCategory.Name
            };

            return featureCategoryDTO;
        }
        public async Task<FeatureCategoryDetailsDTO> GetDetailsAsync(int id)
        {
            var featureCategory = await _featureCategoryRepository.FindDetailsAsync(id);

            FeatureCategoryDetailsDTO featureCategoryDTO = new()
            {
                FeatureCategoryId = featureCategory.FeatureCategoryId,
                CreatedBy = featureCategory.CreatedBy,
                DisplayOrder = featureCategory.DisplayOrder,
                IsMandatory = featureCategory.IsMandatory.Value,
                Name = featureCategory.Name,
                ProductFeatures = featureCategory.ProductFeatures
                                    .Select(pf => new ProductFeatureDTO
                                    {
                                        ProductFeatureId = pf.ProductFeaturesId,
                                        Name = pf.Name,
                                    }).ToList()
            };

            return featureCategoryDTO;
        }
        public async Task<int> AddAsync(FeatureCategoryDTO data)
        {
            if (data.Name == null)
                throw new ArgumentException("Feature category name is required.");

            var userId = _userContextService.GetUserId();
            var uid = userId.ToString();

            FeatureCategory category = new()
            {
                Name = data.Name,
                CreatedBy = uid ?? "000000000",
                IsMandatory = true
            };

            int id = await _featureCategoryRepository.InsertAsync(category);
            return id;
        }

        public async Task<bool> UpdateAsync(FeatureCategoryDTO data)
        {
            if (data == null || data.FeatureCategoryId <= 0)
            {
                _logger.LogError("Invalid FeatureCategory data for update.");
                throw new ArgumentException("Invalid feature category data.");
            }

            var existingFeature = await _featureCategoryRepository.FindByIdAsync(data.FeatureCategoryId);
            if (existingFeature == null)
            {
                _logger.LogWarning("Product feature not found with ID: {Id}", data.FeatureCategoryId);
                return false;
            }

            existingFeature.Name = data.Name;
            existingFeature.IsMandatory = data.IsMandatory;                        

            await _featureCategoryRepository.ModifyAsync(existingFeature);

            return true;
        }
        public async Task<bool> LinkFeatCatToProdCat(int featureCategoryId, int productCategoryId)
        {
            if (productCategoryId <= 0 && featureCategoryId <= 0) throw new ArgumentNullException(nameof(productCategoryId), nameof(featureCategoryId));
            
            return await _featureCategoryRepository.LinkFeatCatToProdCat(featureCategoryId, productCategoryId); 
        }
        public async Task<bool> UnlinkFeatCatProdCat(int productCategoryId, int featureCategoryId)
        {
            if (productCategoryId <= 0 && featureCategoryId <= 0) throw new ArgumentNullException(nameof(productCategoryId), nameof(featureCategoryId));

            await _featureCategoryRepository.UnlinkCategoryFeature(productCategoryId, featureCategoryId);
            return true;
        }

      
        public async Task<bool> DeleteAsync(int id)
        {
            if (id > 0)
            {
                return await _featureCategoryRepository.RemoveAsync(id); ;
            }
            return false;
        }
    }
}
