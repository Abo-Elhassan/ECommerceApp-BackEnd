using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }  
        public decimal Price { get; set; }
        public int NumberOfItem { get; set; }
        public string PictureUrl { get; set; }
        public string Manufacturer { get; set; }
        public string Category { get; set; }
    }
}
