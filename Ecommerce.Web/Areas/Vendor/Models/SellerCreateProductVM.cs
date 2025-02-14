using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Domain.Entities;

namespace eCommerce.Web.Areas.Vendor.Models
{
    public class SellerCreateProductVM
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Url { get; set; }

        public string? BrandName { get; set; }

        public string? CategoryName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
       

        public ProductVariantDTO? ProductVariantDTO { get; set; }

        public IEnumerable<ProductImage>? ProuctImages { get; set; }

        public IEnumerable<ProductConfiguration>? ProductConfigurations { get; set; }
    }
}
