using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Customer
{
    public interface IOrderRepository
    {

        // Get a specific order by its ID
        Task<Order?> GetOrderByIdAsync(Guid orderId);

        // Get all orders for a specific customer
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);

        // Create a new order
        Task<Order> CreateOrderAsync(Order order);

        // Update order status (e.g., Pending → Shipped)
        Task UpdateOrderStatusAsync(Guid orderId, string newStatus);

        // Check if an order belongs to a specific customer (for validation)
        Task<bool> OrderBelongsToCustomerAsync(Guid orderId, Guid customerId);

        // Get total count (useful for pagination)
        Task<int> GetTotalOrdersCountAsync(Guid customerId);
        
    }
}
