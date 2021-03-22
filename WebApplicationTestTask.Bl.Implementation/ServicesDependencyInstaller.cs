using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Bl.Implementation.Services;
using WebApplicationTestTask.Bl.Implementation.Validators;
using WebApplicationTestTask.Models.Customer;
using WebApplicationTestTask.Models.Order;
using WebApplicationTestTask.Models.OrderProduct;
using WebApplicationTestTask.Models.Product;

namespace WebApplicationTestTask.Bl.Implementation
{
    public static class ServicesDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IOrderService, OrderService>();

            //Validators
            services.AddTransient<IValidator<ProductCreationModel>, ProductCreationModelValidator>();
            services.AddTransient<IValidator<ProductModel>, ProductModelValidator>();
            services.AddTransient<IValidator<CustomerCreationModel>, CustomerCreationModelValidator>();
            services.AddTransient<IValidator<OrderProductModel>, OrderProductModelValidator>();
            services.AddTransient<IValidator<OrderCreationalModel>, OrderCreationalModelValidator>();
        }
    }
}
