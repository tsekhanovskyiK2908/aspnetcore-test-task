using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Models.Product;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Bl.Implementation.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductMapper _productMapper;
        public ProductService(IProductRepository productRepository, 
            IProductMapper productMapper)
        {
            _productRepository = productRepository;
            _productMapper = productMapper;
        }
        public async Task<DataResult<Product>> CreateProductAsync(ProductCreationModel productModel)
        {
            Product product = _productMapper.MapBackToEntity(productModel);
            product.CreationDate = DateTime.Now;

            Product addedProduct = await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();

            return new DataResult<Product>
            {
                Data = addedProduct,
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        public async Task<Result> DeleteProductAsync(int productId)
        {
            Product productToDelete = await _productRepository.GetByIdAsync(productId);

            if(productToDelete == null)
            {
                return new Result
                {
                    ResponseMessageType = ResponseMessageType.Error
                };
            }

            await _productRepository.DeleteAsync(productToDelete);
            await _productRepository.SaveAsync();

            return new Result
            {
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        public async Task<DataResult<List<ProductModel>>> GetAllProductsAsync()
        {
            List<Product> products = await _productRepository.GetAllAsync();

            _productMapper.MapToModel(products[0]);

            List<ProductModel> productModels = products.Select(_productMapper.MapToModel).ToList();

            return new DataResult<List<ProductModel>>
            {
                Data = productModels,
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        public async Task<Result> UpdateProductAsync(ProductModel productModel)
        {
            Product productToUpdate = await _productRepository.GetByIdAsync(productModel.Id);

            if(productToUpdate == null)
            {
                return new Result
                {
                    ResponseMessageType = ResponseMessageType.Error,
                    MessageDetails = "Product does not exist"
                };
            }

            productToUpdate = _productMapper.MapBack(productModel, productToUpdate);

            _productRepository.Update(productToUpdate);
            await _productRepository.SaveAsync();

            return new Result
            {
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        public async Task<DataResult<ProductModel>> GetProductById(int productId)
        {
            Product product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return new DataResult<ProductModel>
                {
                    ResponseMessageType = ResponseMessageType.Error,
                    MessageDetails = "Product does not exist"
                };
            }

            ProductModel productModel = _productMapper.MapToModel(product);

            return new DataResult<ProductModel>
            {
                ResponseMessageType = ResponseMessageType.Success,
                Data = productModel
            };
        }
    }
}
