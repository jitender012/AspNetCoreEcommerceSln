using eCommerce.Application.Common.Dtos;
using eCommerce.Application.DTO;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Dtos
{
    public class FeatureCategoryDetailsDTO
    {
        public int FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }

        public int? ProductCategoryName { get; set; }

        public List<IdNameDto<int>>? ProductFeatures { get; set; }
        public List<IdNameDto<int>>? ProductCategories { get; set; }
    }
}
