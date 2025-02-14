using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.ServiceContracts.ProductServiceContracts
{
    public interface IProductConfiguration
    {
        /// <summary>
        /// Create new feature like "Size, Colour" and link with specific category
        /// </summary>
        /// <param name="CategoryId">Id of category to link with</param>
        /// <returns>void</returns>
        Task AddProductFeatureAsync(ProductFeatureDTO productFeature);
        Task AddMultipleProductFeatureAsync(IEnumerable<int> FeatureIds);
        Task AddFeatureCategoryAsync(FeatureCategory category);
        Task<int> LinkFeatureToFeatureCategoryAsync(int featureId, int categoryId);
        Task<int> LinkFeatureToMultipleFeatureCategoriesAsync(int featureId, int featureCategoryIds);
        Task<int> LinkFeatureToCategoryAsync(int featureId, int categoryId);
        Task<int> LinkFeatureToMultipleCategoriesAsync(int featureId, List<int> categoryIds);

        Task<List<ProductFeature>> GetProductFeaturesByCategoryAsync(int categoryId);

        /// <summary>
        /// Add new value to a ProductFeature
        /// </summary>
        /// <param name="productFeatureId">Id of ProductFeature to link</param>
        /// <returns>void</returns>
        Task AddFeatureOptionsAsync(FeatureOptionDTO featureOption);

        /// <summary>
        /// Get all Feature options but Product Feature Id
        /// </summary>
        /// <param name="productFeatureId">Id of Product Feature</param>
        /// <returns>List<FeatureOption></returns>
        Task<List<FeatureOption>> GetFeatureOptionsByProductFeatureAsync(int productFeatureId);
    }
}
