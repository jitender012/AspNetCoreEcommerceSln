using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Web.Areas.Seller.Models;
using eCommerce.Web.ViewModels.ProductFeatureVMs;
using eCommerce.Web.ViewModels.ProductVariantVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.ViewModels.ProductVMs
{
    public class ProductSaveVM
    {
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }

        public string? BrandId { get; set; }

        public int CategoryId { get; set; }

        public ProductVariantSaveVM ProductVariant { get; set; } = new();
        
        public List<FeatureCategoryWithFeaturesDto> FeatureCategory { get; set; } = [];


        public IEnumerable<SelectListItem>? BrandList { get; set; }

        public IEnumerable<SelectListItem>? CategoryList { get; set; }
    }
}
