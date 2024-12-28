using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class Notification
{
    public int NotificationId { get; set; }

    public Guid UserId { get; set; }

    public string Message { get; set; } = null!;

    public byte[]? Type { get; set; }

    public bool? IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
