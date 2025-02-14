using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts.Products
{
    public interface IProductRepository
    {
        Task<Guid> InsertAsync(Product product, ProductVariant productVariant, IEnumerable<ProductImage> productImages, IEnumerable<ProductConfiguration> configurations);
        Task<bool> ModifyAsync(Product product, ProductVariant productVariant, IEnumerable<ProductImage> productImages, IEnumerable<ProductConfiguration> configurations);

        Task<bool> RemoveAsync(Guid productId);
        Task<bool> SoftDeleteProductAsync(Guid productId);

        #region ReadMethods
        Task<IEnumerable<Product>> FetchAllAsync();
        Task<IEnumerable<Product>> FetchBySellerIdAsync(Guid vendorId);
        Task<Product?> FetchByIdAsync(Guid productId);

        Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count);
        Task<IEnumerable<Product>> GetFeaturedProductsAsync();
        Task<IEnumerable<Product>> GetRecentlyAddedProductsAsync(int count);
 
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<IEnumerable<Product>> GetProductsByBrandAsync(Guid brandId);

        #endregion

    }

}
