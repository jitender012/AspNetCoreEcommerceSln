using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class ProductAttribute
{
    public string ProductId { get; set; } = null!;

    public string AttributeName { get; set; } = null!;

    public string AttributeValue { get; set; } = null!;
}
