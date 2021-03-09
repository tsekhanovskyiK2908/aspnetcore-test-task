using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(OrderingSystemDbContext shoppingDbContext) : 
            base(shoppingDbContext)
        {
        }
    }
}
