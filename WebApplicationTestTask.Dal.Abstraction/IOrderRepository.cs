using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Models.Order;

namespace WebApplicationTestTask.Dal.Abstraction
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<OrderPresentationModel> GetOrderPresentationModel(int orderId);
        Task<List<OrderPresentationModel>> GetOrderPresentationModels();
    }
}
