using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Queries
{
    public record ProductCategoryDetailsQuery(int id) : IRequest<ProductCategoryDetailsDto>
    {
    }
}
