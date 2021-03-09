using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Implementation
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(OrderingSystemDbContext shoppingDbContext) : base(shoppingDbContext)
        {
        }
    }
}
