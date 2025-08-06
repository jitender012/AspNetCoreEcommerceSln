using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using MediatR;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Commands
{
    public record CreateCategoryCommand(CreateProductCategoryDto dto) : IRequest<int>
    {
    }
}
