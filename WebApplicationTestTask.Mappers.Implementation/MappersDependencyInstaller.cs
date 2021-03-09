using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Models.Product;

namespace WebApplicationTestTask.Mappers.Implementation
{
    public static class MappersDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddTransient<IProductMapper, ProductMapper>();
            services.AddTransient<ICustomerMapper, CustomerMapper>();
            services.AddTransient<IOrderMapper, OrderMapper>();
        }
    }
}
