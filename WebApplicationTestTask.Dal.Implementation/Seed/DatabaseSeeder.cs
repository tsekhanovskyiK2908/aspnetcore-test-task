using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationTestTask.Dal.Implementation.Seed
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            AddEntities(modelBuilder, SampleData.Products);
            AddEntities(modelBuilder, SampleData.Customers);
            AddEntities(modelBuilder, SampleData.Orders);
            AddEntities(modelBuilder, SampleData.OrderProducts);
        }

        private static void AddEntities<T>(ModelBuilder modelBuilder, List<T> entities) where T : class
        {
            modelBuilder.Entity<T>().HasData(entities);
        }

    }
}
