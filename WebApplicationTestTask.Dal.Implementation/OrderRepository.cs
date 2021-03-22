using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Models.Order;
using WebApplicationTestTask.Models.OrderProduct;

namespace WebApplicationTestTask.Dal.Implementation
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderingSystemDbContext shoppingDbContext) : base(shoppingDbContext)
        {
        }

        public async Task<OrderPresentationModel> GetOrderPresentationModel(int orderId)
        {
            OrderPresentationModel orderPresentationModel = await DbSet
                   .Where(o => o.Id == orderId)                                                            
                   .Include(o => o.Customer)
                   .Select(o => new OrderPresentationModel
                   {
                       Id = o.Id,
                       CustomerName = o.Customer.Name,
                       CustomerAddress = o.Customer.Address,
                       CustomerId = o.CustomerId,
                       Comment = o.Comment,
                       OrderStatus = o.OrderStatus,
                       TotalCost = o.TotalCost,
                       OrderProducts = Context.OrderProducts.Where(op => op.OrderId == o.Id)
                                                            .Include(op => op.Product)
                                                            .Select(op => new OrderProductPresentationModel
                                                            {
                                                                ProductId = op.ProductId,
                                                                ProductName = op.Product.Name,
                                                                ProductCategory = op.Product.ProductCategory,
                                                                Quantity = op.Quantity,
                                                                Price = op.Price,
                                                                ProductPrice = op.Product.Price
                                                            }).ToList()
                   }).FirstOrDefaultAsync();

            return orderPresentationModel;
        }

        public async Task<List<OrderPresentationModel>> GetOrderPresentationModels()
        {
            List<OrderPresentationModel> orderPresentationModels = await DbSet
                   .Include(o => o.Customer)
                   .Select(o => new OrderPresentationModel
                   {
                       Id = o.Id,
                       CustomerName = o.Customer.Name,
                       CustomerAddress = o.Customer.Address,
                       CustomerId = o.CustomerId,
                       Comment = o.Comment,
                       OrderStatus = o.OrderStatus,
                       TotalCost = o.TotalCost,
                       OrderProducts = Context.OrderProducts.Where(op => op.OrderId == o.Id)
                                                            .Include(op => op.Product)
                                                            .Select(op => new OrderProductPresentationModel
                                                            {
                                                                ProductId = op.ProductId,
                                                                ProductName = op.Product.Name,
                                                                ProductCategory = op.Product.ProductCategory,
                                                                Quantity = op.Quantity,
                                                                Price = op.Price,
                                                                ProductPrice = op.Product.Price
                                                            }).ToList()
                   }).ToListAsync();

            return orderPresentationModels;
        }

        public override async Task<List<Order>> GetAllAsync()
        {
            return await DbSet.Include(o => o.Customer).Include(o => o.OrderProducts).ToListAsync();
        }

    }
}
