using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts.Products
{
    public interface IProductFeatureRepository
    {

        #region Common Methods
        /// <summary>
        /// Insert a product feature, must reference a feature category.
        /// </summary>
        /// <param name="productFeature">ProductFeature object</param>
        /// <returns>void</returns>
        Task<int> InsertAsync(ProductFeature productFeature, int featureCategoryId, int productCategoryId = 0);

        Task<IEnumerable<ProductFeature>> FetchAllAsync();

        Task<ProductFeature> FetchByIdAsync(int productFeatureId);

        Task<bool> ModifyAsync(ProductFeature featureCategory);

        Task<bool> RemoveAsync(int productFeatureId);
        #endregion


        /// <summary>
        /// Insert multiple product feature at once,each must reference a feature category.
        /// </summary>
        /// <param name="FeatureIds"></param>
        /// <returns></returns>
        Task InsertMultipleProductFeatureAsync(IEnumerable<ProductFeature> productFeatures);

        Task<int> LinkFeatureToFeatureCategoryAsync(int featureId, int categoryId);

        Task<int> LinkProductFeatureToMultipleFeatureCategoriesAsync(int featureId, List<int> categoryIds);

        Task<int> LinkToSpecificProductCategoryAsync(int featureId, int categoryId);

        Task<List<ProductFeature>> GetProductFeaturesAsync(int categoryId = 0, int productCategoryId = 0);
    }
}
