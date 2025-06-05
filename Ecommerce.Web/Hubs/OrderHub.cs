using Microsoft.AspNetCore.SignalR;

namespace eCommerce.Web.Hubs
{
    public class OrderHub : Hub
    {
        public async Task SendOrderStatus(string orderId, string status)
        {
            await Clients.All.SendAsync("ReceiveOrderStatus", orderId, status);
        }
    }
}
