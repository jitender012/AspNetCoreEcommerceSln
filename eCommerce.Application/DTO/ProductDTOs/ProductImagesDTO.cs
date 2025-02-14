using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO.ProductDTOs
{
    public class ProductImagesDTO
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
