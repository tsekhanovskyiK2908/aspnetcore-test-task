using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Task<decimal> GetProductPrice(int productId)
        {
            return DbSet.Where(p => p.Id == productId)
                        .Select(p => p.Price)
                        .FirstOrDefaultAsync();
        }
    }
}
