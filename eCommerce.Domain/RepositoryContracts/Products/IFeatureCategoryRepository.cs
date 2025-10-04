using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Products
{
    public interface IFeatureCategoryRepository
    {

        #region Basic CRUD operations
        Task<bool> ModifyAsync(FeatureCategory featureCategory);

        #endregion
        #region Insert Methods

        /// <summary>
        /// Create new feature category like "Dimension, Screen Details, etc." and link with specific product category
        /// </summary>
        /// <param name="CategoryId">Id of category to link with</param>
        /// <returns>void</returns>
        Task<int> InsertAsync(FeatureCategory featureCategory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task InsertMultipleProductFeatureCategoryAsync(IEnumerable<FeatureCategory> category);

        #endregion

        #region Read Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        Task<IEnumerable<FeatureCategory>> FetchAllAsync();
        Task<IEnumerable<FeatureCategory>> FetchByProductCategoryIdAsync(int productCategory );
        Task<bool> UnlinkCategoryFeature(int productCategoryId, int featureCategoryId);
        Task<bool> LinkFeatCatToProdCat(int featureCategoryId, int productCategoryId);

        Task<FeatureCategory> FindByIdAsync(int id);
        Task<FeatureCategory> FindDetailsAsync(int id);
        #endregion

        public Task<bool> RemoveAsync(int id);
    }
}
