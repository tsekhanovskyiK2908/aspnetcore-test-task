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
        public Order MapBack(OrderUpdateModel model, Order existing)
        {
            existing.Id = model.Id;
            existing.CustomerId = model.CustomerId;
            existing.Comment = model.Comment;
            existing.OrderStatus = model.OrderStatus;

            return existing;
        }

        public Order MapBackToEntity(OrderCreationalModel model)
        {
            return new Order
            {
                CustomerId = model.CustomerId,
                OrderStatus = model.OrderStatus,
                Comment = model.Comment
            };
        }


        public OrderPresentationModel MapToModel(Order order, Customer customer)
        {
            return new OrderPresentationModel
            {
                Id = order.Id,
                CustomerName = customer.Name,
                CustomerAddress = customer.Address,
                OrderStatus = order.OrderStatus,
                TotalCost = order.TotalCost,
                OrderDate = order.OrderDate
            };
        }
    }
}
