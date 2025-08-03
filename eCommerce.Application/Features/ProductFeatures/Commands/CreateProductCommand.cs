using eCommerce.Application.Features.ProductFeatures.Dtos;
using MediatR;

namespace eCommerce.Application.Features.ProductFeatures.Commands
{
    public record CreateProductCommand(CreateProductDto dto) : IRequest<Guid>
    {
    }
}
