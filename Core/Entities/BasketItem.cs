using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Url]
        public string PictureUrl { get; set; }
        public string Manufacturer { get; set; }
        public string Category { get; set; }
    }
}
