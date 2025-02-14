using eCommerce.Domain.Entities;

namespace eCommerce.Application.DTO.ProductDTOs
{
    public class ProductDTO
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

        public bool? IsDeleted { get; set; }

        public ProductVariantDTO? ProductVariantDTO { get; set; }

        public IEnumerable<ProductImage>? ProuctImages { get; set; }

        public IEnumerable<ProductConfiguration>? ProductConfigurations { get; set; }
    }
}
