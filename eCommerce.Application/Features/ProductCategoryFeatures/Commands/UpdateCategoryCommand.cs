using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using MediatR;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Commands
{
    public record UpdateCategoryCommand(UpdateCategoryDto dto) : IRequest<bool>
    {
    }
}
