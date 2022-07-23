using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Manufacturer { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }



    }
}
