using eCommerce.Application.Features.OrderFeature.Dtos;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Common;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;

namespace eCommerce.Application.Features.OrderFeature.Commands
{
    public record CreateOrderCommand(CreateOrderDto Dto) : IRequest<OrderDto>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICartRepository cartRepository, IProductVariantRepository productVariant)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productVariantRepository = productVariant;
        }

        public Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
