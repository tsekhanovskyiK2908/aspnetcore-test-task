using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities.Enums;

namespace WebApplicationTestTask.Models.Order
{
    public class OrderPresentationModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public decimal TotalCost { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string Comment { get; set; }
    }
}
