using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class ProductParams
    {
        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int TotalItemCount { get; set; } = 0;
        public string CategoryName { get; set; } = "All";
        public string Sort { get; set; } = "name";
        public string SearchQuery { get; set; } = "";
        public List<Product> ProductsReturn { get; set; }= new List<Product>();
    }
}
