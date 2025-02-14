using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO.VendorDTOs
{
    public class SellerProductDTO
    {

        public string ProductName { get; set; } = null!;
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int? TotalStock { get; set; }
        public string? Url { get; set; }

        public List<SellerProductVariantDTO>? ProductVariants { get; set; }
    }
}
