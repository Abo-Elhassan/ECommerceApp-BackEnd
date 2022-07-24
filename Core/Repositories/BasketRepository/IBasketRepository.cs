using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.BasketRepository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasket( string basketId );
        Task<CustomerBasket> UpdateBasket( CustomerBasket basket);
        Task<bool> DeleteBasket( string basketId );
    }
}
