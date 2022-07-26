using Core.Context;
using Core.Entities.OrderAggregate;
using Core.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.DeliveryMethodRepository
{
    public class DeliveryMethodRepository : GenericRepository<DeliveryMethod> , IDeliveryMethodRepository
    {
        private readonly StoreContext _storecontext;

        public DeliveryMethodRepository(StoreContext storecontext) : base(storecontext)
        {
            _storecontext = storecontext;
        }

        public async Task<DeliveryMethod> GetDeliveryMethodByIdAsync(int id)
        {
            return await _storecontext.Set<DeliveryMethod>().FindAsync(id);

        }
    }
}
