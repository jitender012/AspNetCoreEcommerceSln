using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.AddressFeature.Dtos
{
    public class AddressDto
    {
        public int AddressId { get; set; }

        public Guid UserId { get; set; }

        public string? AddressType { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }

        public bool? IsDefault { get; set; }
    }
}
