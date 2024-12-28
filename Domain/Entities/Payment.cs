﻿using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class Payment
{
    public int PaymentId { get; set; }

    public Guid OrderId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
