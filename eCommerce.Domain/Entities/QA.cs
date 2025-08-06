using eCommerce.Domain.IdentityEntities;
using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class QA
{
    public int QueryId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProductVariantId { get; set; }

    public string QueryText { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public Guid? AnsweredBy { get; set; }

    public string? AnswerText { get; set; }

    public DateTime? AnsweredAt { get; set; }

    public virtual ApplicationUser? AnsweredByNavigation { get; set; }

    public virtual ApplicationUser Customer { get; set; } = null!;

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
