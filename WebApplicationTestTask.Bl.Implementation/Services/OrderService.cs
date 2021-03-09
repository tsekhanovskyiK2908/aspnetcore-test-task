using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Models.Order;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Bl.Implementation.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderMapper _orderMapper;

        public OrderService(IOrderRepository orderRepository, 
            ICustomerRepository customerRepository,
            IOrderMapper orderMapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderMapper = orderMapper;
        }

        public async Task<DataResult<List<OrderPresentationModel>>> GetAllOrders()
        {
            List<Order> orders = await _orderRepository.GetAllAsync();
            List<OrderPresentationModel> orderPresentationModels = new List<OrderPresentationModel>();

            foreach (Order order in orders)
            {
                Customer orderCustomer = await _customerRepository.GetByIdAsync(order.CustomerId);

                if(orderCustomer == null)
                {
                    return new DataResult<List<OrderPresentationModel>>
                    {
                        ResponseMessageType = ResponseMessageType.Error,
                        MessageDetails = "Customer of this order does not exist"
                    };
                }

                orderPresentationModels.Add(_orderMapper.MapToModel(order, orderCustomer));
            }

            return new DataResult<List<OrderPresentationModel>>
            {
                Data = orderPresentationModels,
                ResponseMessageType = ResponseMessageType.Success
            };
        }
    }
}
