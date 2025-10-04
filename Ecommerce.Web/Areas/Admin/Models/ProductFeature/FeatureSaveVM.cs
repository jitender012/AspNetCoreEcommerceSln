using eCommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Admin.Models.ProductFeature
{
    public class FeatureSaveVM
    {
        public int? ProductFeatureId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsManadatory { get; set; }
        public FeatureInputType InputType { get; set; }        
        public int FeatureCategoryId { get; set; }
        public int? MeasurementUnitId { get; set; }
        public List<string>? FeatureOptions { get; set; }
        public IEnumerable<SelectListItem>? FeatureCategoryDropdown { get; set; }
        public IEnumerable<SelectListItem>? MeasurementUnitDropdown { get; set; }
        public IEnumerable<SelectListItem>? InputTypeDropdown { get; set; }
    }
}
