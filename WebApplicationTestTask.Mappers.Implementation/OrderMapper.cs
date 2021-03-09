using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Models.Customer;
using WebApplicationTestTask.Models.Order;

namespace WebApplicationTestTask.Mappers.Implementation
{
    public class OrderMapper : IOrderMapper
    {
        public OrderPresentationModel MapToModel(Order order, Customer customer)
        {
            return new OrderPresentationModel
            {
                Id = order.Id,
                CustomerName = customer.Name,
                CustomerAddress = customer.Address,
                OrderStatus = order.OrderStatus,
                TotalCost = order.TotalCost
            };
        }
    }
}
