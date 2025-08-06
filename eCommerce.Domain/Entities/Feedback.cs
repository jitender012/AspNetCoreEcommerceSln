using eCommerce.Domain.IdentityEntities;
using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProductVariantId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ApplicationUser Customer { get; set; } = null!;

    public virtual FeedbackImage? FeedbackImage { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
