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

    public virtual AspNetUser? AnsweredByNavigation { get; set; }

    public virtual AspNetUser Customer { get; set; } = null!;

    public virtual ProductVarient ProductVariant { get; set; } = null!;
}
