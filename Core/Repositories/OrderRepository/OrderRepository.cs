using Core.Context;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Repositories.BasketRepository;
using Microsoft.EntityFrameworkCore;
namespace Core.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IBasketRepository _basketRepo;
        private readonly StoreContext _storeContext;

        public OrderRepository(IBasketRepository basketRepo, StoreContext storeContext)
        {
            _storeContext = storeContext;
            _basketRepo = basketRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int delieveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasket(basketId);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var orderItem = new OrderItem(item.Id, item.Price, item.Quantity,item.ProductName,item.PictureUrl);
                items.Add(orderItem);
            }
            var deliveryMethod = await GetDeliveryMethodsAsync(delieveryMethodId);
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal);
            await _storeContext.AddAsync(order);
            await _storeContext.SaveChangesAsync();
            await _basketRepo.DeleteBasket(basketId);
            order = await _storeContext.Orders.Include(o => o.DeliveryMethod).FirstOrDefaultAsync(o => o.Id == order.Id);
            return order;
        }
        public Task<DeliveryMethod> GetDeliveryMethodsAsync(int deliveryId)
        {
            return _storeContext.DeliveryMethods.FirstOrDefaultAsync(d => d.Id == deliveryId);
        }
        public async Task<List<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
            return await _storeContext.DeliveryMethods.ToListAsync();
        }

        public Order GetOrdersForUserAsync(int id, string buyerEmail)
        {
            return _storeContext.Set<Order>().FirstOrDefault(u => u.Id == id && u.BuyerEmail == buyerEmail);
        }

        public IReadOnlyList<Order> GetOrdersForUserAsync(string buyerEmail)
        {
            return _storeContext.Set<Order>().Where(u => u.BuyerEmail == buyerEmail).ToList();
        }
    }
}
