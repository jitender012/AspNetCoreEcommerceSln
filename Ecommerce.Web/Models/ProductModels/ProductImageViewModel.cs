using eCommerce.Application.DTO.ProductDTOs;

namespace eCommerce.Web.Models.ProductModels
{
    public class ProductImageViewModel
    {
        public int ProductImageId { get; set; }

        public string? ImageUrl { get; set; }

        public Guid ProductVariantId { get; set; }

        public bool IsPrimary { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? Order { get; set; }
    }
}