using Application.Services.OrderService;
using Application.Services.OrderService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;   
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto input)
        {
            await _orderService.CreateOrder(input);
            return Ok();
        }
        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<GetListOrderDto>> GetAllOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }
        [HttpGet("GetOrderById")]
        public async Task<ActionResult<GetOrderDto>> GetOrderById(int id)
        {
            var result = await _orderService.GetOrder(id);
            return Ok(result);
        }
    }
}
