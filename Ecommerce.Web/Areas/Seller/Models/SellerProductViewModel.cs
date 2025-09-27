using eCommerce.Web.ViewModels.ProductVariantVMs;

namespace eCommerce.Web.Areas.Vendor.Models
{
    public class SellerProductViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int? TotalStock { get; set; }
        public string? Url { get; set; }

        public List<ProductVariantSaveVM>? ProductVariants { get; set; }           
    }
}
