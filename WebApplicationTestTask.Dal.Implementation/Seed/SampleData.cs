using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Implementation.Seed
{
    public class SampleData
    {
        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                AvaliableQuantity = 10,
                ProductCategory = Entities.Enums.ProductCategory.Foods,
                Name = "Avocado",
                Price = 20,
                Description = "Delicious",
                CreationDate = new DateTime(2021,02,20,15,10,0)
            },
            new Product
            {
                Id = 2,
                AvaliableQuantity = 30,
                ProductCategory = Entities.Enums.ProductCategory.Foods,
                Name = "Steak",
                Price = 200,
                Description = "Really Delicious",
                CreationDate = new DateTime(2021,02,20,15,10,0)
            },
            new Product
            {
                Id = 3,
                AvaliableQuantity = 5,
                ProductCategory = Entities.Enums.ProductCategory.Clothes,
                Name = "T-shirt",
                Price = 150,
                Description = "Looks good",
                CreationDate = new DateTime(2021,02,20,15,10,0)
            }
        };

        public static List<Customer> Customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Address = "34, Hryhorenka avenue, Kyiv",
                CreatedDate = new DateTime(2021,03,02,16,10,0),
                Name = "Danylo Kozak"
            },
            new Customer
            {
                Id = 2,
                Address = "16, Polyarna street, Kyiv",
                CreatedDate = new DateTime(2021,03,02,19,0,0),
                Name = "Maryna Hetman"
            },
            new Customer
            {
                Id = 3,
                Address = "120, Peremohy avenue, Kyiv",
                CreatedDate = new DateTime(2021,03,02,19,0,0),
                Name = "Denys Ivahnenko"
            }
        };

        public static List<Order> Orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = new DateTime(2021,02,03,10,30,25),
                TotalCost = 500,
                Comment = "Delivery to the door",
                OrderStatus = Entities.Enums.OrderStatus.Delivered
            },
            new Order
            {
                Id = 2,
                CustomerId = 2,
                OrderDate = new DateTime(2021,02,28,14,30,59),
                TotalCost = 350,
                Comment = "Delivery to the office",
                OrderStatus = Entities.Enums.OrderStatus.Paid
            },
            new Order
            {
                Id = 3,
                CustomerId = 3,
                OrderDate = new DateTime(2021,03,01,8,59,50),
                TotalCost = 210,
                Comment = "Call before arriving",
                OrderStatus = Entities.Enums.OrderStatus.New
            }
        };

        public static List<OrderProduct> OrderProducts = new List<OrderProduct>
        {
            new OrderProduct
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 5
            },
            new OrderProduct
            {
                OrderId = 1,
                ProductId = 2,
                Quantity = 2
            },
            new OrderProduct
            {
                OrderId = 2,
                ProductId = 2,
                Quantity = 1
            },
            new OrderProduct
            {
                OrderId = 2,
                ProductId = 3,
                Quantity = 1
            },
            new OrderProduct
            {
                OrderId = 3,
                ProductId = 1,
                Quantity = 3
            },
            new OrderProduct
            {
                OrderId = 3,
                ProductId = 3,
                Quantity = 1
            },
        };
    }
}
