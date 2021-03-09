using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Models.Customer;
using WebApplicationTestTask.Models.Order;

namespace WebApplicationTestTask.Mappers.Abstraction
{
    public interface IOrderMapper
    {
        OrderPresentationModel MapToModel(Order order, Customer customer);
    }
}
