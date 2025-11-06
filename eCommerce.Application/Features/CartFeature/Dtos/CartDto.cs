using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.CartFeature.Dtos
{
    public class CartDto
    {
        public Guid CartId { get; set; }
        public Guid CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
