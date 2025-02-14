namespace eCommerce.Web.Models.ProductModels
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Url { get; set; }

        public Guid? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsDeleted { get; set; }

        public ProductVariantViewModel? productVariantDTO { get; set; }

        public IEnumerable<ProductImageViewModel>? prouctImages { get; set; }

        public IEnumerable<ProductConfigurationViewModel>? productConfigurations { get; set; }
    }

}
