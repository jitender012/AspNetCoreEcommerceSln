using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts.Products
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetProductVariantsAsync();
        Task<Guid> InsertProductVariantAsync(ProductVariant productVariant, IEnumerable<ProductImage> productImages, IEnumerable<FeatureOption> featureOptions);
        Task<bool> UpdateProductVariantAsync(ProductVariant product);
    }
}
