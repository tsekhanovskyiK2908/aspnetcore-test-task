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
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(OrderingSystemDbContext shoppingDbContext) : base(shoppingDbContext)
        {
        }

        public async Task<OrderProduct> GetOrderProductById(int orderId, int productId)
        {
            return await DbSet.FindAsync(orderId, productId);
        }

        public Task<List<OrderProduct>> GetOrderProductsOfProduct(int productId)
        {
            return DbSet.Where(op => op.ProductId == productId).ToListAsync();
        }
    }
}
