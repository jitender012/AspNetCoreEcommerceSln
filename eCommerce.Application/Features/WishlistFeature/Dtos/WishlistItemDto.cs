using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WishlistFeature.Dtos
{
    public class WishlistItemDto
    {        
        public Guid ProductVariantId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string VariantName { get; set; } = null!;

        public decimal ProductPrice { get; set; }

        public string? ProductImageUrl { get; set; }

    }
}
