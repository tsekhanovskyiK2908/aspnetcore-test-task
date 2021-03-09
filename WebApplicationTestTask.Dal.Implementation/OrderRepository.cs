using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Implementation
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderingSystemDbContext shoppingDbContext) : base(shoppingDbContext)
        {
        }
    }
}
