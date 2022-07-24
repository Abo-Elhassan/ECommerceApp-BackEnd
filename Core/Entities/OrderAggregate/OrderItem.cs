using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem // : BaseEntity done
    {
        public OrderItem()
        {
        }

        public OrderItem(int id, ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            Id = id;
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public ProductItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
