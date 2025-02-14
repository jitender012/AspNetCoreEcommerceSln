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
        #region Insert Methods

        /// <summary>
        /// Create new feature category like "Dimension, Screen Details, etc." and link with specific product category
        /// </summary>
        /// <param name="CategoryId">Id of category to link with</param>
        /// <returns>void</returns>
        Task<int> InsertFeatureCategoryAsync(FeatureCategory featureCategory, int productCategoryId);

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
        Task<IEnumerable<FeatureCategory>> FetchAllAsync(int productCategory = 0);
        Task<FeatureCategory> FindByIdAsync(int id);
        #endregion

        public Task<bool> RemoveAsync(int id);
    }
}
