using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid CustomerId { get; set; }

    public DateOnly OrderDate { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal ShippingAmount { get; set; }

    public decimal NetAmount { get; set; }

    public int ShippingAddressId { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public int BillingAddressId { get; set; }

    public string BillingAddress { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public string? UpdatedAt { get; set; }

    public virtual Address BillingAddressNavigation { get; set; } = null!;

    public virtual AspNetUser Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual ICollection<ReturnRequest> ReturnRequests { get; set; } = new List<ReturnRequest>();


    public void Cancel()
    {
        var hoursPassed = (DateTime.UtcNow - DateTime.Parse(CreatedAt)).TotalHours;

        if (hoursPassed > 24)
            throw new InvalidOperationException("Order can only be cancelled within 24 hours.");

        if (OrderStatus == "Cancelled")
            throw new InvalidOperationException("Order already cancelled.");

        OrderStatus = "Cancelled";
        UpdatedAt = DateTime.UtcNow.ToString("s"); // ISO for
                                                   // mat
    }
}
