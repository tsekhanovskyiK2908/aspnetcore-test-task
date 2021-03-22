using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Models.Order;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Bl.Abstraction.Services
{
    public interface IOrderService
    {
        Task<DataResult<List<OrderPresentationModel>>> GetAllOrders();
        Task<DataResult<OrderPresentationModel>> GetOrderById(int orderId);
        Task<DataResult<int>> CreateOrder(OrderCreationalModel orderCreationalModel);
        Task<DataResult<int>> UpdateOrder(OrderUpdateModel orderUpdateModel);
    }
}
