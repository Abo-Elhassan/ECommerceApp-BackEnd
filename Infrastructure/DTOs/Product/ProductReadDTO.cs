using Infrastructure.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTOs.Product
{
    public class ProductReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
       
        public string PictureUrl { get; set; }
        public CategoryReadDTO Category { get; set; }

    }
}
