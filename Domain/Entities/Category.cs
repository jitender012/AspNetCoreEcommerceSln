﻿using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryImage { get; set; }

    public int? ParentCategoryId { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<VariationCategory> VariationCategories { get; set; } = new List<VariationCategory>();
}
