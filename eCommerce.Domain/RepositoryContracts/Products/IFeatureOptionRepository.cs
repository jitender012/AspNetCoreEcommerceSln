using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Products
{
    public interface IFeatureOptionRepository
    {

        /// <summary>
        /// Add new value to a ProductFeature
        /// </summary>
        /// <param name="productFeatureId">Id of ProductFeature to link</param>
        /// <returns>void</returns>
        Task InsertFeatureOptionsAsync(FeatureOption featureOption);

        /// <summary>
        /// Get all Feature options but Product Feature Id
        /// </summary>
        /// <param name="productFeatureId">Id of Product Feature</param>
        /// <returns>List<FeatureOption></returns>
        Task<List<FeatureOption>> GetFeatureOptionsAsync();
        Task<List<FeatureOption>> GetFeatureOptionsByProductFeatureAsync(int productFeatureId);
    }
}
