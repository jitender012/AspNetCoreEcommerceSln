using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain.Entities;

public partial class Banner
{
    public int BannerId { get; set; }

    public string? BannerName { get; set; }

    [StringLength(250)]
    public string? BannerDescription { get; set; }

    public string? ImagePath { get; set; }

    public string? Link { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }
}
