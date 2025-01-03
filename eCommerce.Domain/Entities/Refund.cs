using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class Refund
{
    public long RefundId { get; set; }

    public Guid? OrderId { get; set; }

    public int? ReturnRequestId { get; set; }

    public string? Amount { get; set; }

    public string? RefundMethod { get; set; }

    public string? Status { get; set; }

    public string? InitiatedAt { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ReturnRequest? ReturnRequest { get; set; }
}
