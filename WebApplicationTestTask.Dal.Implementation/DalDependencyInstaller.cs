using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Dal.Abstraction;

namespace WebApplicationTestTask.Dal.Implementation
{
    public static class DalDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderProductRepository, OrderProductRepository>();
        }
    }
}
