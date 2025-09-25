namespace eCommerce.Web.ViewModels.ProductVariantVMs
{
    public class ProductVariantVM
    {
        public Guid ProductIvarientId { get; set; }

        public string? VarientName { get; set; }

        public int? Quantity { get; set; }

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public bool? IsActive { get; set; } = true;

        public List<string> ImageUrls { get; set; } = [];

        public List<FeaturesVM> Features { get; set; } = [];
    }
}
