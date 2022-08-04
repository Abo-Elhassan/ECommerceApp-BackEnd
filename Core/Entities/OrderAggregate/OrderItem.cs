

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem 
    {
        public OrderItem()
        {
        }

        public OrderItem(Guid productId, decimal price, int quantity,string productName, string pictureUrl)
        {

            ProductId = productId;
            Price = price;
            Quantity = quantity;
            ProductName=productName;
            PictureUrl = pictureUrl;
        }
        public int Id { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

    }
}
