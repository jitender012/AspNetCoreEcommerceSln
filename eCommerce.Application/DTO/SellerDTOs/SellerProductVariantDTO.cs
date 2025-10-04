using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO.VendorDTOs
{
    public class SellerProductVariantDTO
    {
        public Guid ProductVariantId { get; set; }
        public string? VarientName { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status{ get; set; }
    }
}
