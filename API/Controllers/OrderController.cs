using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Repositories.OrderRepository;
using Infrastructure.DTOs.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderRepository.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            //validation for empty order
            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);

        }

        [HttpGet]
        public ActionResult<IReadOnlyList<OrderDto>> GetOrdersForUser()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var orders = _orderRepository.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public ActionResult<OrderToReturnDto> GetOrderByIdForUser(int id)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var order =  _orderRepository.GetOrdersForUserAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderToReturnDto>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderRepository.GetDeliveryMethodsAsync());
        }
    }
}
