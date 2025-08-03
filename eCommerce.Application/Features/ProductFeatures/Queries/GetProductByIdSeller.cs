using eCommerce.Application.Features.ProductFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductFeatures.Queries
{
    public class GetProductByIdSeller : IRequest<SellerProductDto>
    {
    }
}
