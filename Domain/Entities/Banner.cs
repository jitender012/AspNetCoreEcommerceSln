using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class Banner
{
    public int BannerId { get; set; }

    public string? BannerName { get; set; }

    public string? ImagePath { get; set; }

    public string? Link { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
