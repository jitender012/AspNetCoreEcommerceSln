using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class AuditLog
{
    public int LogId { get; set; }

    public string? Action { get; set; }

    public string? TableName { get; set; }

    public string? RecordId { get; set; }

    public Guid ChangedBy { get; set; }

    public DateTime ChangeDate { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public virtual AspNetUser ChangedByNavigation { get; set; } = null!;
}
