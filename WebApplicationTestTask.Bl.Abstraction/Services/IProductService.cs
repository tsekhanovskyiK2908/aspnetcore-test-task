using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Models.Product;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Bl.Abstraction.Services
{
    public interface IProductService
    {
        Task<DataResult<List<ProductModel>>> GetAllProductsAsync();
        Task<DataResult<Product>> CreateProductAsync(ProductCreationModel productModel);
        Task<Result> UpdateProductAsync(ProductModel productModel);
        Task<Result> DeleteProductAsync(int productId);
        Task<DataResult<ProductModel>> GetProductById(int productId);
    }
}
