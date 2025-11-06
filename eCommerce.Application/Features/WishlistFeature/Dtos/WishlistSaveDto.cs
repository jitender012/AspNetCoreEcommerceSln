using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WishlistFeature.Dtos
{
    public class WishlistSaveDto
    {
        public int WishlistId { get; set; }

        public Guid ProductVariantId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
