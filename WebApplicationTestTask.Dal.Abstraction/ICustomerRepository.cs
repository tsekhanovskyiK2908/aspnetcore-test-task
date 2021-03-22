using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Models.Customer;

namespace WebApplicationTestTask.Dal.Abstraction
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<CustomerModel> GetCustomerModel(int customerId);
        Task<List<CustomerModel>> GetCustomerModels();
    }
}
