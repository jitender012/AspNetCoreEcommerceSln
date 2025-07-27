using eCommerce.Application.Features.BrandFeature.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Commands
{
    public record UpdateBrandCommand : IRequest<bool>
    {
        public Guid BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }

        public string? BrandDescription { get; set; }

        public Guid UpdatedBy { get; set; }
    }    
}
