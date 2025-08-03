using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Domain.Entities;
using eCommerce.Web.Areas.Seller.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Vendor.Models
{
    public class SellerCreateProductVM
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }        

        public string? BrandId { get; set; }       

        public int CategoryId{ get; set; }       
       

        public SellerProductVariantViewModel? ProductVariant { get; set; }

        public IEnumerable<IFormFile>? ProuctImages { get; set; }

        public IEnumerable<ProductFeaturesVM>? Features { get; set; }
    }
}
