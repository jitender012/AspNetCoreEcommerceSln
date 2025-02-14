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

        public async Task<FeatureCategoryDTO> GetByIdAsync(int id)
        {
            var featureCategory = await _featureCategoryRepository.FindByIdAsync(id);

            FeatureCategoryDTO featureCategoryDTO = new()
            {
                CreatedBy = featureCategory.CreatedBy,
                DisplayOrder = featureCategory.DisplayOrder,
                IsMandatory = featureCategory.IsMandatory,
                Name = featureCategory.Name
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

            int id = await _featureCategoryRepository.InsertFeatureCategoryAsync(category, data.ProductCategoryId.Value);
            return id;
        }

        public Task<bool> UpdateAsync(FeatureCategoryDTO data)
        {
            throw new NotImplementedException();
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
