using eCommerce.Application.DTO.ProductDTOs;

namespace eCommerce.Web.Areas.Vendor.Models
{
    public class SellerProductVariantViewModel
    {

        public Guid ProductVariantId { get; set; }
        public string? VarientName { get; set; }
        public string? SKU { get; set; } 
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }

    }
}