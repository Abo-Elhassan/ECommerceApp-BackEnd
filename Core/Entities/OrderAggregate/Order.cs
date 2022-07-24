using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Order
    {
        public Order()
         {
         }

        public Order(int id, string buyerEmail, string street, string city, string state, string zipcode, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subtotal,string paymentIntentId)
        {
            Id = id;
            BuyerEmail = buyerEmail;
            Street = street;
            City = city;
            State = state;
            Zipcode = zipcode;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        //public Order(int id, IReadOnlyList<OrderItem> orderItems, string buyerEmail,
        //    Address shipToAddress, DeliveryMethod deliveryMethod,
        //    decimal subtotal, string paymentIntentId)
        //  {
        //    Id = id;
        //    BuyerEmail = buyerEmail;
        //    DeliveryMethod = deliveryMethod;
        //    OrderItems = orderItems;
        //    Subtotal = subtotal;
        //    PaymentIntentId = paymentIntentId;
        //}

            public int Id { get; set; }
            public string BuyerEmail { get; set; }
            public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

          //public Address ShipToAddress { get; set; }
          //adding address clas prop here 
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }

            public DeliveryMethod DeliveryMethod { get; set; }
            public IReadOnlyList<OrderItem> OrderItems { get; set; }
            public decimal Subtotal { get; set; }
            public OrderStatus Status { get; set; } = OrderStatus.Pending;
            public string PaymentIntentId { get; set; }

            public decimal GetTotal()
            {
                return Subtotal + DeliveryMethod.Price;
            }
        }
    }
