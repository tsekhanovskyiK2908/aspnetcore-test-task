using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationTestTask.Dal.Implementation;
using WebApplicationTestTask.Dal.Implementation.Seed;

namespace WebApplicationTestTask.Tests.Services
{   

    public class EntityServiceTest
    {
        //protected DbContextOptions<OrderingSystemDbContext> DbContextOptions { get; }
        //protected readonly DbContextOptions<OrderingSystemDbContext> DbContextOptions =
        //    new DbContextOptionsBuilder<OrderingSystemDbContext>()
        //    .UseInMemoryDatabase(databaseName: "OrdersDatabase")
        //    .Options;

        //public EntityServiceTest()
        //{
        //    //Seed();
        //}
        //public EntityServiceTest()
        //{   
        //    DbContextOptions = new DbContextOptionsBuilder<OrderingSystemDbContext>()
        //    .UseInMemoryDatabase(databaseName: "OrdersDatabase")
        //    .Options;


        //    Seed();
        //}   

        //protected static DbContextOptionsBuilder<OrderingSystemDbContext> CreateNewContextOptionsBuilder()
        //{
        //    ServiceProvider serviceProvider = new ServiceCollection()
        //        .AddEntityFrameworkInMemoryDatabase()
        //        .BuildServiceProvider();

        //    string dbName = Guid.NewGuid().ToString();

        //    DbContextOptionsBuilder<OrderingSystemDbContext> builder = new DbContextOptionsBuilder<OrderingSystemDbContext>();
        //    builder.UseInMemoryDatabase(dbName) //databaseName: "OrdersDatabase"
        //            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        //            .EnableSensitiveDataLogging()    
        //            .UseInternalServiceProvider(serviceProvider);

        //    return builder;
        //}

        protected static DbContextOptions<OrderingSystemDbContext> CreateNewContextOptions()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            string dbName = Guid.NewGuid().ToString();

            DbContextOptionsBuilder<OrderingSystemDbContext> builder = new DbContextOptionsBuilder<OrderingSystemDbContext>();
            builder.UseInMemoryDatabase(dbName, new InMemoryDatabaseRoot()) //databaseName: "OrdersDatabase"
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected static OrderingSystemDbContext CreateCleanDbContext()
        {
            return new OrderingSystemDbContext(CreateNewContextOptions());
        }

        protected void Seed(OrderingSystemDbContext orderingSystemDbContext)
        {
            foreach (var product in SampleData.Products)
            {
                orderingSystemDbContext.Add(product);
                orderingSystemDbContext.SaveChanges();
                orderingSystemDbContext.Entry(product).State = EntityState.Detached;
            }

            foreach (var customer in SampleData.Customers)
            {
                orderingSystemDbContext.Add(customer);
                orderingSystemDbContext.SaveChanges();
                orderingSystemDbContext.Entry(customer).State = EntityState.Detached;
            }

            foreach (var order in SampleData.Orders)
            {
                orderingSystemDbContext.Add(order);
                orderingSystemDbContext.SaveChanges();
                orderingSystemDbContext.Entry(order).State = EntityState.Detached;
            }

            foreach (var orderProduct in SampleData.OrderProducts)
            {
                orderingSystemDbContext.Add(orderProduct);
                orderingSystemDbContext.SaveChanges();
                orderingSystemDbContext.Entry(orderProduct).State = EntityState.Detached;
            }

            //orderingSystemDbContext.Products.AddRange(SampleData.Products);
            ////orderingSystemDbContext.SaveChanges();
            //orderingSystemDbContext.Orders.AddRange(SampleData.Orders);
            ////orderingSystemDbContext.SaveChanges();
            //orderingSystemDbContext.Customers.AddRange(SampleData.Customers);
            ////orderingSystemDbContext.SaveChanges();
            //orderingSystemDbContext.OrderProducts.AddRange(SampleData.OrderProducts);

            //orderingSystemDbContext.SaveChanges();
        }

    }
}
