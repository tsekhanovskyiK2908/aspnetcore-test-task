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

        public CustomerService(ICustomerRepository customerRepository,
            ICustomerMapper customerMapper)
        {
            _customerRepository = customerRepository;
            _customerMapper = customerMapper;
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
            List<Customer> customers = await _customerRepository.GetAllAsync();

            List<CustomerModel> customerModels = customers.Select(_customerMapper.MapToModel).ToList();

            return new DataResult<List<CustomerModel>>
            {
                Data = customerModels,
                ResponseMessageType = ResponseMessageType.Success
            };
        }

        public async Task<DataResult<CustomerModel>> GetCustomer(int customerId)
        {
            Customer customer = await _customerRepository.GetByIdAsync(customerId);

            if(customer == null)
            {
                return new DataResult<CustomerModel>
                {
                    MessageDetails = "Customer don't exists",
                    ResponseMessageType = ResponseMessageType.Error
                };
            }

            CustomerModel customerModel = _customerMapper.MapToModel(customer);

            return new DataResult<CustomerModel>
            {
                Data = customerModel,
                ResponseMessageType = ResponseMessageType.Success
            };
        }
    }
}
