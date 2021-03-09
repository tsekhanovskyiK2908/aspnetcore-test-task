using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Implementation
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(OrderingSystemDbContext shoppingDbContext) : base(shoppingDbContext)
        {
        }
    }
}
