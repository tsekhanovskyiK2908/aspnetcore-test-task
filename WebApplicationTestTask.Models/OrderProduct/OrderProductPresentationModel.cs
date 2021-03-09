using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities.Enums;

namespace WebApplicationTestTask.Models.OrderProduct
{
    public class OrderProductPresentationModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductSize ProductSize { get; set; }

    }
}
