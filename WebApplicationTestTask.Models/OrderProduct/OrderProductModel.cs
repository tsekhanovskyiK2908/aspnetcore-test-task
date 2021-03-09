using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities.Enums;

namespace WebApplicationTestTask.Models.OrderProduct
{
    public class OrderProductModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductSize ProductSize { get; set; }
    }
}
