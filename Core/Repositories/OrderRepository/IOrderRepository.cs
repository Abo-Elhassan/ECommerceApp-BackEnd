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

        
       
        IReadOnlyList<Order> GetOrdersForUserAsync(string buyerEmail);
        Order GetOrdersForUserAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<Order> CreateOrderAsync(string email, int deliveryMethodId, string basketId, Address address);
    }
}
