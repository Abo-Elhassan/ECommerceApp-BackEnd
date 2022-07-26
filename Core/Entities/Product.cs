using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]

        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Url]
        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }
        public string Manufacturer { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }



    }
}
