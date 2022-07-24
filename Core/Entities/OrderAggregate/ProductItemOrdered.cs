using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    //because the prod in order may be would change sp we need a relation between them itself
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemOrderedId, string productName, string pictureUrl)
        {
            ProductItemOrderedId = productItemOrderedId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        //there's no productid prop because it dependent on the order it self (product owned by Order)
        public int ProductItemOrderedId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
