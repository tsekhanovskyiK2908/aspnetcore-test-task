using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Abstraction
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<decimal> GetProductPrice(int productId);
    }
}
