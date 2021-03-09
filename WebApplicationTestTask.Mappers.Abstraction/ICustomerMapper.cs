using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction.Base;
using WebApplicationTestTask.Models.Customer;

namespace WebApplicationTestTask.Mappers.Abstraction
{
    public interface ICustomerMapper : IMapFromModel<CustomerCreationModel, Customer>, IMapToModel<Customer, CustomerModel>
    {

    }
}
