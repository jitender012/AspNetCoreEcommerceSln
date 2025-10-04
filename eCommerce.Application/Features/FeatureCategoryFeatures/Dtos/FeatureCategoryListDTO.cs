using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Dtos
{
    public class FeatureCategoryListDTO
    {

        public int FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }

        public int? ProductCategoryName { get; set; }
    }
}
