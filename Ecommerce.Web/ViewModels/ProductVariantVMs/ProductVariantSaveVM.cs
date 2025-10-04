using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Domain.Entities;

namespace eCommerce.Web.ViewModels.ProductVariantVMs
{
   
    public class ProductVariantSaveVM
    {
        public string? VarientName { get; set; }
        public string? SKU { get; set; } 
        public int? Quantity { get; set; }
        public decimal Price { get; set; }        
        public ProductStatus ProductStatus { get; set; }
        public List<IFormFile>? ProuctImages { get; set; } = new();
    }
}