using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Models.Product;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductCreationModel> _productCreationModelValidator;
        private readonly IValidator<ProductModel> _productModelValidator;

        public ProductsApiController(IProductService productService, 
            IValidator<ProductCreationModel> productCreationModelValidator,
            IValidator<ProductModel> productModel)
        {
            _productService = productService;
            _productCreationModelValidator = productCreationModelValidator;
            _productModelValidator = productModel;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            DataResult<ProductModel> dataResult = await _productService.GetProductById(id);

            if(dataResult.Data == null)
            {
                return NotFound(dataResult);
            }

            return Ok(dataResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreationModel productCreationModel)
        {
            ValidationResult validationResult = _productCreationModelValidator.Validate(productCreationModel);

            if (!validationResult.IsValid)
            {
                DataResult<Product> dataResult = new DataResult<Product>
                {
                    ResponseMessageType = ResponseMessageType.Error,
                    MessageDetails = "Model is invalid",
                    Data = null
                };

                return BadRequest(dataResult);
            }

            return Ok(await _productService.CreateProductAsync(productCreationModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProductModel productModel)
        {
            if(id != productModel.Id)
            {
                return BadRequest(new Result
                {
                    ResponseMessageType = ResponseMessageType.Error,
                    MessageDetails = "Id's don't match"
                });
            }

            ValidationResult validationResult = _productModelValidator.Validate(productModel);

            if(!validationResult.IsValid)
            {
                return BadRequest(new Result
                {
                    ResponseMessageType = ResponseMessageType.Error
                });
            }

            Result result = await _productService.UpdateProductAsync(productModel);

            if(result.ResponseMessageType == ResponseMessageType.Error)
            {
                return NotFound(result);
            }

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Result result = await _productService.DeleteProductAsync(id);

            if (result.ResponseMessageType == ResponseMessageType.Error)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}