using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Abstraction
{
    public interface IOrderProductRepository : IRepository<OrderProduct>
    {
        Task<OrderProduct> GetOrderProductById(int orderId, int productId);
        Task<List<OrderProduct>> GetOrderProductsOfProduct(int productId);
    }
}
