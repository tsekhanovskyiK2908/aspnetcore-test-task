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
            modelBuilder.Entity<OrderProduct>().Property(op => op.Price).HasPrecision(18, 2);

            modelBuilder.Entity<Product>().Property(pr => pr.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(tc => tc.TotalCost).HasPrecision(18, 2);

            modelBuilder.Entity<OrderProduct>()
                        .HasOne<Order>(op => op.Order)
                        .WithMany(o => o.OrderProducts)
                        .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                        .HasOne<Product>(op => op.Product)
                        .WithMany(o => o.OrderProducts)
                        .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<Order>()
                        .HasOne<Customer>(o => o.Customer)
                        .WithMany(c => c.Orders)
                        .HasForeignKey(o => o.CustomerId);

            modelBuilder.SeedDatabase();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
