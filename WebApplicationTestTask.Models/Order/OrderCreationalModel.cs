using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities.Enums;
using WebApplicationTestTask.Models.OrderProduct;

namespace WebApplicationTestTask.Models.Order
{
    public class OrderCreationalModel
    {
        public int CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderProductModel> OrderProductModels { get; set; }
        public string Comment { get; set; }
    }
}
