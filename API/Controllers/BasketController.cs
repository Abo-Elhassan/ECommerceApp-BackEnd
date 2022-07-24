using Core.Entities;
using Core.Repositories.BasketRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseController
    {
        
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository BasketRepository)
        {
            _basketRepository = BasketRepository;
        }
        [HttpGet]

        public async  Task<ActionResult<CustomerBasket>> GetBasketById( string id)
        {
            var basket=  await _basketRepository.GetBasket(id);
            

            return Ok( basket ?? new CustomerBasket(id));
             //return new customerbasket and use id customer generate
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updateBasket = await _basketRepository.UpdateBasket(basket);
            return Ok(updateBasket);
        }
        [HttpDelete]
         public async Task DeleteBasket(string id)
        {
             await _basketRepository.DeleteBasket(id);
        }
    } }

