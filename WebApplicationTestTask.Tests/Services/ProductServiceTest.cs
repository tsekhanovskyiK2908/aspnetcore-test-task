using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Bl.Implementation.Services;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Dal.Implementation;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Models.Product;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Tests.Services
{
    [TestFixture]
    public class ProductServiceTest : EntityServiceTest
    {
        private Mock<IProductMapper> _productMapperMock;
        private IProductRepository _productRepository;
        private IOrderProductRepository _orderProductRepository;
        private OrderingSystemDbContext _orderingSystemDbContext;
        private IProductService _productService;


        [SetUp]
        public void SetUp()
        {
            _orderingSystemDbContext = CreateCleanDbContext();
            Seed(_orderingSystemDbContext);
            _productRepository = new ProductRepository(_orderingSystemDbContext);
            _orderProductRepository = new OrderProductRepository(_orderingSystemDbContext);
            _productMapperMock = new Mock<IProductMapper>();
        }

        [Test]
        [TestCase(2)]
        public async Task ShouldReturnProductById(int productId)
        {
            SetupProductMapper();

            _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);
            Product productExpected = _orderingSystemDbContext.Products.Find(productId);

            DataResult<ProductModel> dataResult = await _productService.GetProductById(productId);

            Assert.IsNotNull(dataResult.Data);
            Assert.AreEqual(ResponseMessageType.Success, dataResult.ResponseMessageType);
            Assert.IsInstanceOf<ProductModel>(dataResult.Data);
            Assert.AreEqual(productExpected.Name, dataResult.Data.Name);
            Assert.AreEqual(productExpected.Id, dataResult.Data.Id);
        }

        [Test]
        [TestCase(100)]
        public async Task ShouldReturnNullProductById(int productId)
        {
            SetupProductMapper();

            _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);

            DataResult<ProductModel> dataResult = await _productService.GetProductById(productId);

            Assert.IsNull(dataResult.Data);
            Assert.AreEqual(ResponseMessageType.Error, dataResult.ResponseMessageType);
        }

        [Test]
        public async Task ShouldReturnAllProducts()
        {
            SetupProductMapper();
            _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);
            int productsCount = _orderingSystemDbContext.Products.Count();

            DataResult<List<ProductModel>> dataResult = await _productService.GetAllProductsAsync();

            Assert.Greater(dataResult.Data.Count, 0);
            Assert.AreEqual(productsCount, dataResult.Data.Count);
        }

        [Test]
        public async Task ShouldCreateProduct()
        {
            ProductCreationModel productCreationModel = new ProductCreationModel
            {
                AvaliableQuantity = 20,
                Name = "Bounty",
                Price = 34m,
                ProductCategory = Entities.Enums.ProductCategory.Foods
            };

            SetupProductMapper();
            _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);
            int maxId = _orderingSystemDbContext.Products.Max(p => p.Id);

            DataResult<Product> dataResult = await _productService.CreateProductAsync(productCreationModel);

            //Assert.Greater(dataResult.Data.Id, maxId);
            //Assert.AreEqual(dataResult.Data.Id, 4);

            Assert.AreEqual(dataResult.Data.Id, maxId + 1);
        }

        //[Test]
        //public async Task ShouldNotCreateProduct()
        //{
        //    ProductCreationModel productCreationModel = new ProductCreationModel
        //    {
        //        AvaliableQuantity = 20,
        //        //Name = "Bounty",
        //        //Price = 34m,
        //        ProductCategory = Entities.Enums.ProductCategory.Foods
        //    };

        //    SetupProductMapper();
        //    _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);

        //    DataResult<Product> dataResult = await _productService.CreateProductAsync(productCreationModel);

        //    Assert.AreEqual(0, dataResult.Data.Id);
        //}

        [Test]
        [TestCase(2)]
        public async Task ShouldNotDeleteProduct(int productId)
        {
            _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);
            int initialCount = _orderingSystemDbContext.Products.Count();

            Result result = await _productService.DeleteProductAsync(productId);
            int countAfterTryDelete = _orderingSystemDbContext.Products.Count();

            Assert.AreEqual(ResponseMessageType.Error, result.ResponseMessageType);
            Assert.AreEqual(initialCount, countAfterTryDelete);
            Assert.AreEqual(3, countAfterTryDelete);
        }

        [Test]
        public async Task ShouldDeleteProduct()
        {
            ProductCreationModel productCreationModel = new ProductCreationModel
            {
                AvaliableQuantity = 20,
                Name = "Bounty",
                Price = 34m,
                ProductCategory = Entities.Enums.ProductCategory.Foods
            };
            SetupProductMapper();
            _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);
            int maxId = _orderingSystemDbContext.Products.Max(p => p.Id);
            int initialProductsCount = _orderingSystemDbContext.Products.Count();

            DataResult<Product> dataResultCreation = await _productService.CreateProductAsync(productCreationModel);
            int addedProductId = dataResultCreation.Data.Id;
            int productsCountBeforeDelete = _orderingSystemDbContext.Products.Count();

            Result deleteResult = await _productService.DeleteProductAsync(addedProductId);
            int productsCountAfterDelete = _orderingSystemDbContext.Products.Count();

            Assert.AreEqual(ResponseMessageType.Success, dataResultCreation.ResponseMessageType);
            Assert.AreEqual(maxId + 1, addedProductId);
            Assert.AreEqual(ResponseMessageType.Success, deleteResult.ResponseMessageType);
            Assert.AreEqual(initialProductsCount, productsCountAfterDelete);
            Assert.AreEqual(productsCountBeforeDelete - 1, productsCountAfterDelete);
        }

        [Test]
        public async Task ShouldNotUpdateProduct()
        {
            ProductModel productModel = new ProductModel
            {
                Id = 20,
                Name = "Bounty",
                CreatedDate = DateTime.UtcNow,
                ProductCategory = Entities.Enums.ProductCategory.Foods,
                AvaliableQuantity = 20,
                Price = 17m,
            };
            SetupProductMapper();
            _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);
            Product productToUpdate = _orderingSystemDbContext.Products.Find(productModel.Id);

            Result updateResult = await _productService.UpdateProductAsync(productModel);

            Assert.IsNull(productToUpdate);
            Assert.AreEqual(ResponseMessageType.Error, updateResult.ResponseMessageType);
        }

        [Test]
        public async Task ShouldUpdateProduct()
        {
            ProductModel productModel = new ProductModel
            {
                Id = 2,
                Name = "Bounty",
                CreatedDate = DateTime.UtcNow,
                ProductCategory = Entities.Enums.ProductCategory.Foods,
                AvaliableQuantity = 20,
                Price = 17m,
            };

            SetupProductMapper();
            _productService = new ProductService(_productRepository, _productMapperMock.Object, _orderProductRepository);
            Product productToUpdate = _orderingSystemDbContext.Products.Find(productModel.Id);

            Result updateResult = await _productService.UpdateProductAsync(productModel);
            Product updatedProduct = _orderingSystemDbContext.Products.Find(productModel.Id);

            Assert.IsNotNull(productToUpdate);
            Assert.AreEqual(ResponseMessageType.Success, updateResult.ResponseMessageType);
            Assert.AreEqual(productModel.Name, updatedProduct.Name);
            Assert.AreEqual(productModel.AvaliableQuantity, updatedProduct.AvaliableQuantity);
            Assert.AreEqual(productModel.Price, updatedProduct.Price);
        }

        [TearDown]
        public void TearDown()
        {
            _orderingSystemDbContext.Database.EnsureDeleted();
        }



        private void SetupProductMapper()
        {
            _productMapperMock.Setup(pm => pm.MapToModel(It.IsAny<Product>()))
                               .Returns((Product p) => new ProductModel
                               {
                                   Id = p.Id,
                                   AvaliableQuantity = p.AvaliableQuantity,
                                   CreatedDate = p.CreationDate,
                                   Name = p.Name,
                                   Price = p.Price,
                                   ProductCategory = p.ProductCategory,
                                   Description = p.Description
                               });

            _productMapperMock.Setup(pm => pm.MapBackToEntity(It.IsAny<ProductCreationModel>()))
                              .Returns((ProductCreationModel pcm) => new Product
                              {
                                  Name = pcm.Name,
                                  ProductCategory = pcm.ProductCategory,
                                  AvaliableQuantity = pcm.AvaliableQuantity,
                                  Price = pcm.Price,
                                  Description = pcm.Description,
                              });

            _productMapperMock.Setup(pm => pm.MapBack(It.IsAny<ProductModel>(), It.IsAny<Product>()))
                              .Returns((ProductModel model, Product existingProduct) =>
                              {
                                  existingProduct.Name = model.Name;
                                  existingProduct.Price = model.Price;
                                  existingProduct.AvaliableQuantity = model.AvaliableQuantity;
                                  existingProduct.ProductCategory = model.ProductCategory;
                                  existingProduct.Description = model.Description;

                                  return existingProduct;
                              });
        }
    }
}
