using eCommerce.Application.Features.BrandFeature.Dtos;
using MediatR;

namespace eCommerce.Application.Features.BrandFeature.Commands
{
    public record CreateBrandCommand(CreateBrandDto Dto) : IRequest<Guid>;   
}
