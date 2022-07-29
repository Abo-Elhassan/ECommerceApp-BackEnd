using Infrastructure.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class ProductParamDTO
    {
        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int TotalItemCount { get; set; } = 0;
        public string CategoryName { get; set; } = "All";
        public string Sort { get; set; } = "name";
        public string SearchQuery { get; set; } = "";
        public List<ProductReadDTO> ProductsReturn { get; set; } = new List<ProductReadDTO>();
    }
}
