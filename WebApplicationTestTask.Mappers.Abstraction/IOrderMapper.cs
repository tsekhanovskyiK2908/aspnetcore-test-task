using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction.Base;
using WebApplicationTestTask.Models.Customer;
using WebApplicationTestTask.Models.Order;

namespace WebApplicationTestTask.Mappers.Abstraction
{
    public interface IOrderMapper : IMapFromModel<OrderCreationalModel, Order>, IMapForUpdate<OrderUpdateModel, Order>
    {
        OrderPresentationModel MapToModel(Order order, Customer customer);
    }
}
