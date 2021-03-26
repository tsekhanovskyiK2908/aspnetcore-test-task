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
using WebApplicationTestTask.Mappers.Abstraction.Base;
using WebApplicationTestTask.Models.Order;
using WebApplicationTestTask.Models.OrderProduct;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Tests.Services
{
    [TestFixture]
    public class OrderServiceTest : EntityServiceTest
    {
        private OrderingSystemDbContext _orderingSystemDbContext;
        private IOrderRepository _orderRepository;
        private Mock<IOrderMapper> _orderMapper;
        private Mock<IMapFromModel<OrderProductModel, OrderProduct>> _orderProductMapper;
        private IProductRepository _productRepository;
        private IOrderProductRepository _orderProductRepository;
        private ICustomerRepository _customerRepository;
        private IOrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _orderingSystemDbContext = CreateCleanDbContext();
            Seed(_orderingSystemDbContext);

            _orderRepository = new OrderRepository(_orderingSystemDbContext);
            _productRepository = new ProductRepository(_orderingSystemDbContext);
            _orderProductRepository = new OrderProductRepository(_orderingSystemDbContext);
            _customerRepository = new CustomerRepository(_orderingSystemDbContext);
            _orderMapper = new Mock<IOrderMapper>();
            _orderProductMapper = new Mock<IMapFromModel<OrderProductModel, OrderProduct>>();
            _orderService = new OrderService(_orderRepository, _orderMapper.Object,
                                _orderProductMapper.Object, _productRepository, _orderProductRepository, _customerRepository);
        }

        [Test]
        public async Task ShouldReturnAllOrders()
        {

            int orderCountInDb = _orderingSystemDbContext.Orders.Count();

            DataResult<List<OrderPresentationModel>> dataResult = await _orderService.GetAllOrders();

            Assert.AreEqual(ResponseMessageType.Success, dataResult.ResponseMessageType);
            Assert.AreEqual(orderCountInDb, dataResult.Data.Count);
        }

        [Test]
        [TestCaseSource("_correctOrdersToCreate")]
        public async Task ShouldAddOrder(Entities.Enums.OrderStatus orderStatus, int customerId, List<OrderProductModel> orderProductModels)
        {
            SetUpMappers();
            OrderCreationalModel orderCreationalModel = new OrderCreationalModel
            {
                OrderStatus = orderStatus,
                CustomerId = customerId,
                OrderProductModels = orderProductModels
            };
            int expectedOrdersCount = _orderingSystemDbContext.Orders.Count() + 1;
            int expectedOrderProductsCount = _orderingSystemDbContext.OrderProducts.Count() + orderCreationalModel.OrderProductModels.Count;
            int expectedOrderId = _orderingSystemDbContext.Orders.Max(o => o.Id) + 1;

            List<OrderProduct> expectedOrderProducts = new List<OrderProduct>();
            List<Product> productsInOrder = new List<Product>();
            decimal expectedTotalCost = 0m;

            foreach (var orderProductModel in orderCreationalModel.OrderProductModels)
            {
                Product product = await _orderingSystemDbContext.Products.FindAsync(orderProductModel.ProductId);
                product.AvaliableQuantity -= orderProductModel.Quantity;
                productsInOrder.Add(product);

                decimal orderProductPrice = product.Price * orderProductModel.Quantity;
                OrderProduct orderProduct = new OrderProduct
                {
                    ProductId = orderProductModel.ProductId,
                    Price = orderProductPrice,
                    OrderId = expectedOrderId,
                    Quantity = orderProductModel.Quantity
                };

                expectedOrderProducts.Add(orderProduct);

                expectedTotalCost += orderProductPrice;
            }

            DataResult<int> dataResult = await _orderService.CreateOrder(orderCreationalModel);
            int actualOrdersCount = _orderingSystemDbContext.Orders.Count();
            int actualOrderProductCount = _orderingSystemDbContext.OrderProducts.Count();
            decimal actualOrderTotalCost = (await _orderingSystemDbContext.Orders.FindAsync(dataResult.Data)).TotalCost;

            Assert.AreEqual(ResponseMessageType.Success, dataResult.ResponseMessageType);
            Assert.AreEqual(orderCreationalModel.OrderProductModels.Count, productsInOrder.Count);
            Assert.AreEqual(expectedOrdersCount, actualOrdersCount);
            Assert.AreEqual(expectedOrderProductsCount, actualOrderProductCount);
            Assert.AreEqual(expectedOrderId, dataResult.Data);

            foreach (Product productInOrder in productsInOrder)
            {
                Product alteredProduct = await _orderingSystemDbContext.Products.FindAsync(productInOrder.Id);
                Assert.GreaterOrEqual(alteredProduct.AvaliableQuantity, 0);
                Assert.AreEqual(productInOrder.AvaliableQuantity, alteredProduct.AvaliableQuantity); 
            }

            foreach (OrderProduct orderProduct in expectedOrderProducts)
            {
                OrderProduct addedOrderProduct = await _orderingSystemDbContext.OrderProducts.FindAsync(dataResult.Data, 
                    orderProduct.ProductId);

                Assert.IsNotNull(addedOrderProduct);
                Assert.Greater(addedOrderProduct.Quantity, 0);
                Assert.AreEqual(orderProduct.Quantity, addedOrderProduct.Quantity);
                Assert.Greater(addedOrderProduct.Price, 0m);
                Assert.AreEqual(orderProduct.Price, addedOrderProduct.Price);
            }

            Assert.AreEqual(expectedTotalCost, actualOrderTotalCost);
        }

        [Test]
        [TestCaseSource("_badOrdersToCreate")]
        public async Task ShouldNotAddOrder(Entities.Enums.OrderStatus status, int customerId, List<OrderProductModel> orderProductModels)
        {
            SetUpMappers();

            OrderCreationalModel orderCreationalModel = new OrderCreationalModel
            {
                OrderStatus = status,
                CustomerId = customerId,
                OrderProductModels = orderProductModels
            };

            DataResult<int> dataResult = await _orderService.CreateOrder(orderCreationalModel);

            Assert.AreEqual(ResponseMessageType.Error, dataResult.ResponseMessageType);
            Assert.AreEqual(0, dataResult.Data);
        }

        [Test]
        [TestCaseSource("_badOrdersToUpdate")]
        public async Task ShouldNotUpdateOrder(int id, Entities.Enums.OrderStatus status, int customerId, List<OrderProductModel> orderProductModels)
        {
            SetUpMappers();

            OrderUpdateModel orderUpdateModel = new OrderUpdateModel
            {
                Id = id,
                OrderStatus = status,
                CustomerId = customerId,
                OrderProductModels = orderProductModels
            };

            DataResult<int> dataResult = await _orderService.UpdateOrder(orderUpdateModel);

            Assert.AreEqual(ResponseMessageType.Error, dataResult.ResponseMessageType);
            Assert.AreEqual(0, dataResult.Data);
        }

        [Test]
        [TestCaseSource("_correctOrdersToUpdate")]
        public async Task ShouldUpdateOrder(int id, Entities.Enums.OrderStatus status, int customerId, List<OrderProductModel> orderProductModels)
        {
            SetUpMappers();

            OrderUpdateModel orderUpdateModel = new OrderUpdateModel
            {
                Id = id,
                OrderStatus = status,
                CustomerId = customerId,
                OrderProductModels = orderProductModels
            };

            List<Product> expectedProducts = new List<Product>();
            List<OrderProduct> expectedOrderProducts = new List<OrderProduct>();

            decimal orderTotalCostExpected = (await _orderingSystemDbContext.Orders.FindAsync(orderUpdateModel.Id)).TotalCost;

            foreach (var orderProductModel in orderUpdateModel.OrderProductModels)
            {
                OrderProduct orderProductToUpdate = await _orderingSystemDbContext.OrderProducts.FindAsync(orderUpdateModel.Id,
                    orderProductModel.ProductId);

                if (orderProductToUpdate != null)
                {   
                    // !!! IMPORTANT TO PREVENT UPDATE ENTITY FROM DB !!! //
                    _orderingSystemDbContext.Entry(orderProductToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                    Product productInOrder = await _orderingSystemDbContext.Products.FindAsync(orderProductToUpdate.ProductId);

                    int quantityDifference = orderProductModel.Quantity - orderProductToUpdate.Quantity;
                    decimal priceDifference = productInOrder.Price * quantityDifference;

                    orderProductToUpdate.Price += priceDifference;
                    orderProductToUpdate.Quantity += quantityDifference;
                    orderTotalCostExpected += priceDifference;
                    
                    productInOrder.AvaliableQuantity -= quantityDifference;
                    expectedProducts.Add(productInOrder);
                }
                else
                {
                    Product productInOrder = await _orderingSystemDbContext.Products.FindAsync(orderProductModel.ProductId);

                    orderProductToUpdate = new OrderProduct
                    {
                        Price = productInOrder.Price * orderProductModel.Quantity,
                        OrderId = orderUpdateModel.Id,
                        ProductId = productInOrder.Id,
                        Quantity = orderProductModel.Quantity
                    };

                    productInOrder.AvaliableQuantity -= orderProductModel.Quantity;
                    orderTotalCostExpected += orderProductToUpdate.Price;
                    expectedProducts.Add(productInOrder);

                }

                expectedOrderProducts.Add(orderProductToUpdate);
            }


            DataResult<int> dataResult = await _orderService.UpdateOrder(orderUpdateModel);
            int actualOrderProductsCount = _orderingSystemDbContext.OrderProducts.Where(op => op.OrderId == dataResult.Data)
                                                                           .Count();
            decimal actualOrderTotalCost = (await _orderingSystemDbContext.Orders.FindAsync(dataResult.Data)).TotalCost;

            Assert.AreEqual(ResponseMessageType.Success, dataResult.ResponseMessageType);
            Assert.AreEqual(orderUpdateModel.Id, dataResult.Data);
            Assert.LessOrEqual(orderUpdateModel.OrderProductModels.Count, actualOrderProductsCount);

            foreach (var expectedProduct in expectedProducts)
            {
                Product actualProduct = await _orderingSystemDbContext.Products.FindAsync(expectedProduct.Id);

                Assert.GreaterOrEqual(actualProduct.AvaliableQuantity, 0);
                Assert.AreEqual(expectedProduct.AvaliableQuantity, actualProduct.AvaliableQuantity);
            }

            foreach (var expectedOrderProduct in expectedOrderProducts)
            {
                OrderProduct addedOrderProduct = await _orderingSystemDbContext.OrderProducts.FindAsync(dataResult.Data,
                    expectedOrderProduct.ProductId);

                Assert.IsNotNull(addedOrderProduct);
                Assert.Greater(addedOrderProduct.Quantity, 0);
                Assert.AreEqual(expectedOrderProduct.Quantity, addedOrderProduct.Quantity);
                Assert.Greater(addedOrderProduct.Price, 0m);
                Assert.AreEqual(expectedOrderProduct.Price, addedOrderProduct.Price);
            }

            Assert.AreEqual(orderTotalCostExpected, actualOrderTotalCost);
        }

        private static object[] _badOrdersToCreate =
        {
            new object[] {Entities.Enums.OrderStatus.New, 200, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                }},
            new object[] {Entities.Enums.OrderStatus.New, 1, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1000,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                }},
            new object[] {Entities.Enums.OrderStatus.New, 2, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 200,
                        Quantity = 3,
                    },
                }
            }
        };

        private static object[] _badOrdersToUpdate =
        {
            new object[] {150, Entities.Enums.OrderStatus.New, 2, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                }},
            new object[] {1, Entities.Enums.OrderStatus.New, 200, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                }},
            new object[] {2, Entities.Enums.OrderStatus.New, 1, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1000,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                }},
            new object[] {3, Entities.Enums.OrderStatus.New, 2, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 200,
                        Quantity = 3,
                    },
                }
            }
        };

        private static object[] _correctOrdersToUpdate =
        {
            new object[] {1, Entities.Enums.OrderStatus.New, 2, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                    new OrderProductModel
                    {
                        ProductId = 3,
                        Quantity = 2,
                    },
                }},
            new object[] {2, Entities.Enums.OrderStatus.New, 3, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                }},
            new object[] {2, Entities.Enums.OrderStatus.New, 1, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 3,
                        Quantity = 2,
                    },
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 4,
                    },
                }},
            new object[] {3, Entities.Enums.OrderStatus.New, 2, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 3,
                        Quantity = 3,
                    },
                }
            }
        };

        private static object[] _correctOrdersToCreate =
        {
            new object[] {Entities.Enums.OrderStatus.New, 3, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                }},
            new object[] {Entities.Enums.OrderStatus.Paid, 1, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 3,
                    }
                }},
            new object[] {Entities.Enums.OrderStatus.Shipped, 2, new List<OrderProductModel>
                {
                    new OrderProductModel
                    {
                        ProductId = 1,
                        Quantity = 1,
                    },
                    new OrderProductModel
                    {
                        ProductId = 2,
                        Quantity = 3,
                    },
                    new OrderProductModel
                    {
                        ProductId = 3,
                        Quantity = 1,
                    },
                }
            }
        };

        public void SetUpMappers()
        {
            _orderMapper.Setup(om => om.MapBackToEntity(It.IsAny<OrderCreationalModel>()))
                .Returns((OrderCreationalModel model) =>
                {
                    return new Order
                    {
                        CustomerId = model.CustomerId,
                        OrderStatus = model.OrderStatus,
                        Comment = model.Comment
                    };
                });

            _orderMapper.Setup(om => om.MapBack(It.IsAny<OrderUpdateModel>(), It.IsAny<Order>()))
                        .Returns((OrderUpdateModel model, Order existing) =>
                        {
                            existing.Id = model.Id;
                            existing.CustomerId = model.CustomerId;
                            existing.Comment = model.Comment;
                            existing.OrderStatus = model.OrderStatus;

                            return existing;
                        });

            _orderProductMapper.Setup(opm => opm.MapBackToEntity(It.IsAny<OrderProductModel>()))
                               .Returns((OrderProductModel model) =>
                               {
                                   return new OrderProduct
                                   {
                                       ProductId = model.ProductId,
                                       ProductSize = model.ProductSize,
                                       Quantity = model.Quantity
                                   };
                               });
        }
    }
}
