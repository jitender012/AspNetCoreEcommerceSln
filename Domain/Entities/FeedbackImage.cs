using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class FeedbackImage
{
    public int FeedbackImageId { get; set; }

    public int FeedbackId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Feedback FeedbackImageNavigation { get; set; } = null!;
}
