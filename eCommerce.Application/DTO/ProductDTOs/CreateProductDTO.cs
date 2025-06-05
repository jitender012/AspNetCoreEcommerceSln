using eCommerce.Application.DTO.VendorDTOs;
using eCommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO.ProductDTOs
{
    public class CreateProductDTO
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Url { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        public SellerProductVariantDTO? ProductVariant { get; set; }

        public IEnumerable<IFormFile>? ProuctImages { get; set; }

        public IEnumerable<ProductConfiguration>? ProductConfigurations { get; set; }
    }
}
