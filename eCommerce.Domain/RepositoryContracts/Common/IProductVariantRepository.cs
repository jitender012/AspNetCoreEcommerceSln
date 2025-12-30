using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts.Common
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetProductVariantsAsync();
        Task<List<ProductVariant>> GetProductVariantBySellerId(Guid sellerId);
        Task<ProductVariant?> GetProductVariantByIdAsync(Guid productVariantId);
        Task<Guid> InsertProductVariantAsync(ProductVariant productVariant, IEnumerable<ProductImage> productImages, IEnumerable<FeatureOption> featureOptions);
        Task<bool> UpdateProductVariantAsync(ProductVariant product);
    }
}
