using eCommerce.Application.Common.Dtos;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Features.ProductFeatures.Dtos 
{ 
    public class FeaturesDto
    {
        public int ProductFeatureId { get; set; }        
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string? MeasurementUnit { get; set; }

        public FeatureInputType InputType { get; set; }

        public List<IdNameDto<int>> FeatureOptions { get; set; } = new();
    }
}
