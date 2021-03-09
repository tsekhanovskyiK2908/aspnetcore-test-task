using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Models.Customer;

namespace WebApplicationTestTask.Mappers.Implementation
{
    public class CustomerMapper : ICustomerMapper
    {
        public Customer MapBackToEntity(CustomerCreationModel model)
        {
            return new Customer
            {
                Name = model.Name,
                Address = model.Address
            };
        }

        public CustomerModel MapToModel(Customer entity)
        {
            return new CustomerModel
            {   
                Id = entity.Id,
                Address = entity.Address,
                Name = entity.Name
            };
        }
    }
}
