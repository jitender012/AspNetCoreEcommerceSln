using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductConfigurationFeature.DTOs
{
    public class FeatureListDTO
    {
        public int ProductFeaturesId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsManadatory { get; set; }
        public FeatureInputType InputType { get; set; }
        public string? FeatureCategoryName { get; set; }
        public string? MeasurementUnit { get; set; }
    }
}
