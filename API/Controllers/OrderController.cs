using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Repositories.OrderRepository;
using Infrastructure.DTOs.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderWriteDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var address = _mapper.Map<Address>(orderDto.ShipToAddress);

            var order = await _orderRepository.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            //validation for empty order
            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "Problem creating order"));
            }
               

            return Ok(_mapper.Map<OrderReadDto>(order));

        }

        [HttpGet]
        public ActionResult<IReadOnlyList<OrderReadDto>> GetOrdersForUser()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var orders = _orderRepository.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderReadDto>>(orders));
        }

        [HttpGet("{id}")]
        public ActionResult<OrderReadDto> GetOrderByIdForUser(int id)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var order =  _orderRepository.GetOrdersForUserAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderReadDto>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderRepository.GetDeliveryMethodsAsync());
        }
    }
}
