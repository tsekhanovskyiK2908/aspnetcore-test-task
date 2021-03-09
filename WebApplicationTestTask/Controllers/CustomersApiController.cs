using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Models.Customer;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerCreationModel> _customerCreationModelValidator;

        public CustomersApiController(ICustomerService customerService, 
            IValidator<CustomerCreationModel> customerCreationModelValidator)
        {
            _customerService = customerService;
            _customerCreationModelValidator = customerCreationModelValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _customerService.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            DataResult<CustomerModel> dataResult = await _customerService.GetCustomer(id);

            if(dataResult.Data == null)
            {
                return NotFound(dataResult);
            }

            return Ok(dataResult);
        }

        [HttpPost]    
        public async Task<IActionResult> Post([FromBody] CustomerCreationModel customerCreationModel)
        {
            ValidationResult validationResult = _customerCreationModelValidator.Validate(customerCreationModel);

            if(!validationResult.IsValid)
            {
                DataResult<CustomerModel> dataResult = new DataResult<CustomerModel>
                {
                    ResponseMessageType = ResponseMessageType.Error,
                };

                return BadRequest(dataResult);
            }

            return Ok(await _customerService.CreateCustomer(customerCreationModel));
        }

    }
}
