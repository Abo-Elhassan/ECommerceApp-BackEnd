using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Basket
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="Price Must be geater than zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,double.MaxValue,ErrorMessage ="Numbe of Item must be at least one")]
        public int NumberOfItem { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
