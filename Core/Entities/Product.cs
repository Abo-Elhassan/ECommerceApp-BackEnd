using Core.Entities.OrderAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Url]
       
        public string PictureUrl { get; set; }
        public string Manufacturer { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public ICollection<OrderItem> orderItems { get; set; } = new HashSet<OrderItem>();




    }
}
