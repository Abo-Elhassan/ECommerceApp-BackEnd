using Core.Entities;
using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.OrderRepository
{
    public interface IOrderRepository
    {

        
        Task<Order> CreateOrderAsync(Order order, int delieveryMethodId, string basketId);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task CreateOrderAsync(string email, int deliveryMethodId, string basketId, Address address);
    }
}
