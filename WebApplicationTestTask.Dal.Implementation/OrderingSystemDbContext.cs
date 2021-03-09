using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Dal.Implementation.Seed;
using WebApplicationTestTask.Entities;

namespace WebApplicationTestTask.Dal.Implementation
{
    public class OrderingSystemDbContext : DbContext
    {        
        public OrderingSystemDbContext(DbContextOptions<OrderingSystemDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });
            modelBuilder.Entity<Product>().Property(pr => pr.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(tc => tc.TotalCost).HasPrecision(18, 2);

            modelBuilder.SeedDatabase();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
