using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Mappers.Abstraction.Base;
using WebApplicationTestTask.Models.Order;
using WebApplicationTestTask.Models.OrderProduct;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Bl.Implementation.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderMapper _orderMapper;
        private readonly IMapFromModel<OrderProductModel, OrderProduct> _orderProductMapper;
        private readonly IProductRepository _productRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository,
            IOrderMapper orderMapper,
            IMapFromModel<OrderProductModel, OrderProduct> orderProductMapper,
            IProductRepository productRepository,
            IOrderProductRepository orderProductRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _orderMapper = orderMapper;
            _orderProductMapper = orderProductMapper;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
            _customerRepository = customerRepository;
        }

        // Map model to order
        // Set date to order
        // Calculate total cost and OrderProduct prices
        // Insert order
        // Set orderId to orderProducts
        // Insert orderProducts
        public async Task<DataResult<int>> CreateOrder(OrderCreationalModel orderCreationalModel)
        {
            using (IDbContextTransaction transaction = await _orderRepository.BeginTransactionAsync())
            {
                try
                {
                    Customer orderCustomer = await _customerRepository.GetByIdAsync(orderCreationalModel.CustomerId);

                    if (orderCustomer == null)
                    {
                        return new DataResult<int>
                        {
                            ResponseMessageType = ResponseMessageType.Error,
                            MessageDetails = "Customer of order does not exists"
                        };
                    }

                    Order order = _orderMapper.MapBackToEntity(orderCreationalModel);
                    order.OrderDate = DateTime.UtcNow;

                    order = await _orderRepository.AddAsync(order);
                    await _orderRepository.SaveAsync();

                    foreach (OrderProductModel orderProductModel in orderCreationalModel.OrderProductModels)
                    {
                        Result orderProductAddResult = await AddOrderProductAsync(order, orderProductModel);

                        if (orderProductAddResult.ResponseMessageType == ResponseMessageType.Error)
                        {
                            await transaction.RollbackAsync();

                            return new DataResult<int>
                            {
                                ResponseMessageType = ResponseMessageType.Error
                            };
                        }
                    }

                    _orderRepository.Update(order);
                    await _orderRepository.SaveAsync();

                    await transaction.CommitAsync();

                    return new DataResult<int>
                    {
                        Data = order.Id,
                        ResponseMessageType = ResponseMessageType.Success
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<DataResult<List<OrderPresentationModel>>> GetAllOrders()
        {
            List<OrderPresentationModel> orderPresentationModels = await _orderRepository.GetOrderPresentationModels();

            return new DataResult<List<OrderPresentationModel>>
            {
                Data = orderPresentationModels,
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        public async Task<DataResult<OrderPresentationModel>> GetOrderById(int orderId)
        {
            OrderPresentationModel orderPresentationModel = await _orderRepository.GetOrderPresentationModel(orderId);

            if (orderPresentationModel == null)
            {
                return new DataResult<OrderPresentationModel>
                {
                    ResponseMessageType = ResponseMessageType.Error
                };
            }

            return new DataResult<OrderPresentationModel>
            {
                Data = orderPresentationModel,
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        // take order from database
        // update order
        // take all order product
        // update 
        public async Task<DataResult<int>> UpdateOrder(OrderUpdateModel orderUpdateModel)
        {
            Order orderToUpdate = await _orderRepository.GetByIdAsync(orderUpdateModel.Id);

            if (orderToUpdate == null)
            {
                return new DataResult<int>
                {
                    ResponseMessageType = ResponseMessageType.Error,
                    MessageDetails = "Order you trying to update does not exist"
                };
            }

            using (IDbContextTransaction transaction = await _orderRepository.BeginTransactionAsync())
            {
                try
                {
                    Customer orderCustomer = await _customerRepository.GetByIdAsync(orderUpdateModel.CustomerId);

                    if (orderCustomer == null)
                    {
                        return new DataResult<int>
                        {
                            ResponseMessageType = ResponseMessageType.Error,
                            MessageDetails = "Customer of order does not exists"
                        };
                    }

                    orderToUpdate = _orderMapper.MapBack(orderUpdateModel, orderToUpdate);

                    foreach (OrderProductModel orderProductModel in orderUpdateModel.OrderProductModels)
                    {
                        OrderProduct orderProductToUpdate =
                            await _orderProductRepository.GetOrderProductById(orderToUpdate.Id, orderProductModel.ProductId);

                        if (orderProductToUpdate == null)
                        {
                            Result orderProductAddResult = await AddOrderProductAsync(orderToUpdate, orderProductModel);

                            if (orderProductAddResult.ResponseMessageType == ResponseMessageType.Error)
                            {
                                await transaction.RollbackAsync();

                                return new DataResult<int>
                                {
                                    ResponseMessageType = ResponseMessageType.Error
                                };
                            }
                        }
                        else
                        {
                            Product product = await _productRepository.GetByIdAsync(orderProductModel.ProductId);

                            if (product == null)
                            {
                                await transaction.RollbackAsync();

                                return new DataResult<int>
                                {
                                    ResponseMessageType = ResponseMessageType.Error,
                                    MessageDetails = "Product from order does not exist"
                                };
                            }

                            int quantityDifference = orderProductModel.Quantity - orderProductToUpdate.Quantity;
                            decimal priceDifference = product.Price * quantityDifference;

                            if (quantityDifference != 0)
                            {
                                if (quantityDifference <= product.AvaliableQuantity)
                                {
                                    orderProductToUpdate.Price += priceDifference;
                                    orderProductToUpdate.Quantity = orderProductModel.Quantity;
                                    orderToUpdate.TotalCost += priceDifference;

                                    _orderProductRepository.Update(orderProductToUpdate);
                                }
                                else
                                {
                                    await transaction.RollbackAsync();

                                    return new DataResult<int>
                                    {
                                        ResponseMessageType = ResponseMessageType.Error
                                    };
                                }

                                _orderRepository.Update(orderToUpdate);

                                product.AvaliableQuantity -= quantityDifference;
                                _productRepository.Update(product);

                                await _productRepository.SaveAsync();
                            }

                        }
                    }

                    await transaction.CommitAsync();
                    return new DataResult<int>
                    {
                        Data = orderToUpdate.Id,
                        ResponseMessageType = ResponseMessageType.Success
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task<Result> AddOrderProductAsync(Order order, OrderProductModel orderProductModel)
        {
            OrderProduct orderProduct = _orderProductMapper.MapBackToEntity(orderProductModel);
            orderProduct.OrderId = order.Id;

            Product product = await _productRepository.GetByIdAsync(orderProduct.ProductId);

            if (product == null)
            {
                return new Result
                {
                    ResponseMessageType = ResponseMessageType.Error
                };
            }

            if (orderProduct.Quantity <= product.AvaliableQuantity)
            {
                orderProduct.Price = product.Price * orderProduct.Quantity;
                order.TotalCost += orderProduct.Price;
                product.AvaliableQuantity -= orderProduct.Quantity;

                await _orderProductRepository.AddAsync(orderProduct);
                _productRepository.Update(product);
                await _orderProductRepository.SaveAsync();

                return new Result
                {
                    ResponseMessageType = ResponseMessageType.Success
                };
            }

            return new Result
            {
                ResponseMessageType = ResponseMessageType.Error
            };

        }
    }
}
