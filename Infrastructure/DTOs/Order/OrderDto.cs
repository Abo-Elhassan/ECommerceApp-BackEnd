using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Order
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
