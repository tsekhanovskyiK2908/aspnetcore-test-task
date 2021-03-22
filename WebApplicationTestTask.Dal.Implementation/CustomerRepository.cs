using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Models.Customer;

namespace WebApplicationTestTask.Dal.Implementation
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(OrderingSystemDbContext shoppingDbContext) : base(shoppingDbContext)
        {
        }

        public Task<CustomerModel> GetCustomerModel(int customerId)
        {
            return DbSet.Where(c => c.Id == customerId).Select(c => new CustomerModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                OrdersCount = Context.Orders.Where(o => o.CustomerId == c.Id).Count(),
                TotalOrderedCost = Context.Orders.Where(o => o.CustomerId == c.Id).Sum(o => o.TotalCost)
            }).FirstOrDefaultAsync();
        }

        public Task<List<CustomerModel>> GetCustomerModels()
        {
            return DbSet.Select(c => new CustomerModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                OrdersCount = Context.Orders.Where(o => o.CustomerId == c.Id).Count(),
                TotalOrderedCost = Context.Orders.Where(o => o.CustomerId == c.Id).Sum(o => o.TotalCost)
            }).ToListAsync();
        }
    }
}
