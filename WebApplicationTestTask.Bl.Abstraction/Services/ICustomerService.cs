using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Models.Customer;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Bl.Abstraction.Services
{
    public interface ICustomerService
    {   
        Task<DataResult<CustomerModel>> CreateCustomer(CustomerCreationModel customerCreationModel);
        Task<DataResult<List<CustomerModel>>> GetAllCustomers();
        Task<DataResult<CustomerModel>> GetCustomer(int customerId);
    }
}
