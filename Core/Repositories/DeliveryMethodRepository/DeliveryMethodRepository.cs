using Core.Context;
using Core.Entities.OrderAggregate;
using Core.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.DeliveryMethodRepository
{
    public class DeliveryMethodRepository : GenericRepository<DeliveryMethod>
    {
        private readonly StoreContext _storecontext;

        public DeliveryMethodRepository(StoreContext storecontext) : base(storecontext)
        {
            _storecontext = storecontext;
        }

       

       
    }
}
