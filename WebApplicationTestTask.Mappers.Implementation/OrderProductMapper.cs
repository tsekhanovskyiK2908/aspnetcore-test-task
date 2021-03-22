using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction.Base;
using WebApplicationTestTask.Models.OrderProduct;

namespace WebApplicationTestTask.Mappers.Implementation
{
    public class OrderProductMapper : IMapFromModel<OrderProductModel, OrderProduct>
    {
        public OrderProduct MapBackToEntity(OrderProductModel model)
        {
            return new OrderProduct
            {
                ProductId = model.ProductId,
                ProductSize = model.ProductSize,
                Quantity = model.Quantity
            };
        }
    }
}
