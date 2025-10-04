using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public class ProductCategoryProductFeature
    {
        public int ProductCategoryId { get; set; }
        public int ProductFeatureId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; } = null!;
        public virtual ProductFeature ProductFeature { get; set; } = null!;
    }
}
