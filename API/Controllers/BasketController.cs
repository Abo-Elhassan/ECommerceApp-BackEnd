using AutoMapper;
using Core.Entities;
using Core.Repositories.BasketRepository;
using Infrastructure.DTOs.Basket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseController
    {
        
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository BasketRepository,IMapper mapper)
        {
            _basketRepository = BasketRepository;
            _mapper = mapper;
        }
        [HttpGet]

        public async  Task<ActionResult<CustomerBasket>> GetBasketById( string id)
        {
            var basket=  await _basketRepository.GetBasket(id);
            

            return Ok( basket ?? new CustomerBasket(id));
             //return new customerbasket and use id customer generate
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            var customerBasket=_mapper.Map<CustomerBasket>(basket);
            var updateBasket = await _basketRepository.UpdateBasket(customerBasket);
            return Ok(updateBasket);
        }
        [HttpDelete]
         public async Task DeleteBasket(string id)
        {
             await _basketRepository.DeleteBasket(id);
        }
    } }

