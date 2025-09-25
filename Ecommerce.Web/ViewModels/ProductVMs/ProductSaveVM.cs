using eCommerce.Web.Areas.Seller.Models;
using eCommerce.Web.ViewModels.ProductVariantVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.ViewModels.ProductVMs
{
    public class ProductSaveVM
    {

        public string? Description { get; set; }

        public string? BrandId { get; set; }

        public int CategoryId { get; set; }

        public CreateProductVariantVM ProductVariant { get; set; } = new();

        public IEnumerable<ProductFeaturesVM>? Features { get; set; }

        public IEnumerable<SelectListItem>? BrandList { get; set; }

        public IEnumerable<SelectListItem>? CategoryList { get; set; }
    }
}
