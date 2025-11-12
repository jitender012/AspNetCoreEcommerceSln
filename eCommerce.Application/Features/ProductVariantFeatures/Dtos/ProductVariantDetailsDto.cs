using AutoMapper.Features;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductVariantFeatures.Dtos
{
    public class ProductVariantDetailsDto
    {
        public Guid ProductIvarientId { get; set; }

        public string? VariantName { get; set; }
        
        public decimal Price { get; set; }

        public string? Barcode { get; set; }

        public int? Quantity { get; set; }

        public string Sku { get; set; } = null!;

        public bool? IsActive { get; set; } = true;

        public List<string?> ImageUrls { get; set; } = [];

        public List<ProductFeatureDto> Features { get; set; } = [];
        //public int TotalSold { get; set; }
    }
}
