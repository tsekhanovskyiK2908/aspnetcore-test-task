using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Abstraction
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
