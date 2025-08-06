using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Queries
{
    public record GetAllCategories : IRequest<List<ProductCategoryDto>>
    {
    }
}
