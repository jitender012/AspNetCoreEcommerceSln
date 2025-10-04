using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;
public enum ProductStatus
{
    Draft,
    Active,
    Inactive,
    Archived
}
public partial class ProductVariant
{
    public Guid ProductVariantId { get; set; }

    public string? VarientName { get; set; }

    public Guid ProductId { get; set; }    

    public string Sku { get; set; } = null!;

    public decimal Price { get; set; }
    
    public int? SortOrder { get; set; } 

    public string? Barcode { get; set; }

    public ProductStatus Status { get; set; } = ProductStatus.Draft;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductConfiguration> ProductConfigurations { get; set; } = new List<ProductConfiguration>();

    public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<QA> QAs { get; set; } = new List<QA>();

    public virtual ICollection<ReturnRequest> ReturnRequests { get; set; } = new List<ReturnRequest>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
