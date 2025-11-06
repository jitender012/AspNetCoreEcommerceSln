using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.OrderFeature.Dtos
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "COD";
    }
}
