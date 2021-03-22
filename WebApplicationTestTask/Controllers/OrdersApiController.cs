using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<OrderCreationalModel> _orderCreationalModelValidator;

        public OrdersApiController(IOrderService orderService,
            IValidator<OrderCreationalModel> orderCreationalModelValidator)
        {
            _orderService = orderService;
            _orderCreationalModelValidator = orderCreationalModelValidator;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            DataResult<OrderPresentationModel> dataResult = await _orderService.GetOrderById(id);

            if(dataResult.ResponseMessageType == ResponseMessageType.Error)
            {
                return BadRequest(dataResult);
            }

            return Ok(dataResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderCreationalModel orderCreationalModel)
        {
            ValidationResult validationResult = _orderCreationalModelValidator.Validate(orderCreationalModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(new DataResult<int> { 
                    ResponseMessageType = ResponseMessageType.Error
                });
            }

            return Ok(await _orderService.CreateOrder(orderCreationalModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] OrderUpdateModel orderUpdateModel)
        {
            if(id != orderUpdateModel.Id)
            {
                return BadRequest(new DataResult<int>
                {
                    ResponseMessageType = ResponseMessageType.Error
                });
            }

            ValidationResult validationResult = _orderCreationalModelValidator.Validate(orderUpdateModel);

            if(!validationResult.IsValid)
            {
                return BadRequest(new DataResult<int>
                {
                    ResponseMessageType = ResponseMessageType.Error
                });
            }

            DataResult<int> dataResult = await _orderService.UpdateOrder(orderUpdateModel);

            if(dataResult.ResponseMessageType == ResponseMessageType.Error)
            {
                return BadRequest(dataResult);
            }

            return Ok(dataResult);
        }
    }
}
