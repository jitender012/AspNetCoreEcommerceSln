using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.DTO.ProductDTOs
{
    public class FeatureOptionDTO
    {

        public int FeatureOptionId { get; set; }
        public int ProductFeatureId { get; set; }
        public string Value { get; set; } = null!;
        public string ProductFeatureName { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        
        public FeatureOption ToDomain()
        {
            FeatureOption featureOption = new FeatureOption()
            {
                FeatureOptionId = FeatureOptionId,
                ProductFeatureId = ProductFeatureId,
                Value = Value,
                CreatedBy = CreatedBy
            };

            return featureOption;
        }

        public static FeatureOptionDTO ToDto(FeatureOption featureOption)
        {
            FeatureOptionDTO featureOptionDTO = new()
            {
                FeatureOptionId = featureOption.FeatureOptionId,
                ProductFeatureId = featureOption.ProductFeatureId,
                Value = featureOption.Value,
                ProductFeatureName = featureOption.ProductFeature.Name,
                CreatedBy = featureOption.CreatedBy
            };
            return featureOptionDTO;
        }

        public static List<FeatureOptionDTO> ToDtoList(List<FeatureOption> featureOptions) 
        {
            return featureOptions.Select(f=> ToDto(f)).ToList();
        }
    }   
}
