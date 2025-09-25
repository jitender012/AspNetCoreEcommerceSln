using eCommerce.Web.ViewModels.ProductVariantVMs;

namespace eCommerce.Web.ViewModels.ProductVMs
{
    public class ProductDetailsVM
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Url { get; set; }

        public string CategoryName { get; set; } = null!;

        public string BrandName { get; set; } = null!;

        public List<ProductVariantVM> ProductVariants { get; set; } = [];
    }
}
