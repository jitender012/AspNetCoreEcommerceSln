using eCommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Admin.Models.Product
{
    public class ProductFeatureViewModel
    {
        public int ProductFeatureId { get; set; }        
        public string Name { get; set; } = null!;
        public bool IsManadatory { get; set; }
        public FeatureInputType InputType { get; set; }        
        public int FeatureCategoryId { get; set; }
        public string? FeatureCategoryName { get; set; }         
        public List<FeatureOptionsViewModel>? FeatureOptions { get; set; }

        public IEnumerable<SelectListItem>? FeatureCategoryDropdown { get; set; }
    }
}
