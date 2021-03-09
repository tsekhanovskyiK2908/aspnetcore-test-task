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
    }
}
