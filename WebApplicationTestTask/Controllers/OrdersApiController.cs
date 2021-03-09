using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Models.Order;
using WebApplicationTestTask.Models.Product;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            DataResult<List<OrderPresentationModel>> dataResult = await _orderService.GetAllOrders();

            if(dataResult == null)
            {
                return BadRequest(dataResult);
            }

            return Ok(dataResult);
        }
    }
}
