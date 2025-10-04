using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductConfigurationFeature.DTOs
{
    public  class FeatureSaveDTO
    {
        public int? ProductFeatureId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsManadatory { get; set; }
        public FeatureInputType InputType { get; set; }        
        public int FeatureCategoryId { get; set; }
        public int? MeasurementUnitId { get; set; }
        public List<string>? FeatureOptions { get; set; }
    }
}
