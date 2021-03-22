using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Mappers.Abstraction.Base;
using WebApplicationTestTask.Models.Customer;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Bl.Implementation.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerMapper _customerMapper;
        private readonly IOrderRepository _orderRepository;

        public CustomerService(ICustomerRepository customerRepository,
            ICustomerMapper customerMapper,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _customerMapper = customerMapper;
            _orderRepository = orderRepository;
        }
        public async Task<DataResult<CustomerModel>> CreateCustomer(CustomerCreationModel customerCreationModel)
        {
            Customer customer = _customerMapper.MapBackToEntity(customerCreationModel);
            customer.CreatedDate = DateTime.UtcNow;

            Customer addedCustomer = await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveAsync();

            CustomerModel customerModel = _customerMapper.MapToModel(addedCustomer);

            return new DataResult<CustomerModel>
            {
                Data = customerModel,
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        public async Task<DataResult<List<CustomerModel>>> GetAllCustomers()
        {
            List<CustomerModel> customerModels = await _customerRepository.GetCustomerModels();

            return new DataResult<List<CustomerModel>>
            {
                Data = customerModels,
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        public async Task<DataResult<CustomerModel>> GetCustomer(int customerId)
        {
            CustomerModel customerModel = await _customerRepository.GetCustomerModel(customerId);

            if (customerModel == null)
            {
                return new DataResult<CustomerModel>
                {
                    MessageDetails = "Customer don't exists",
                    ResponseMessageType = ResponseMessageType.Error
                };
            }

            return new DataResult<CustomerModel>
            {
                Data = customerModel,
                ResponseMessageType = ResponseMessageType.Success
            };
        }
    }
}
