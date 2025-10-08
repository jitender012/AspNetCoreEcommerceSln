using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Products
{
    public interface IProductConfigurationRepository
    {
        Task<bool> LinkFeatureToCategoryAsync(List<ProductCategoryProductFeature> productCategoryProductFeatures);
        Task<bool> UnlinkFeatureFromCategoryAsync(List<ProductCategoryProductFeature> productCategoryProductFeatures);        

    }
}
