using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities.Enums;
using WebApplicationTestTask.Models.OrderProduct;

namespace WebApplicationTestTask.Models.Order
{
    public class OrderPresentationModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public decimal TotalCost { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string Comment { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderProductPresentationModel> OrderProducts { get; set; }
    }
}
