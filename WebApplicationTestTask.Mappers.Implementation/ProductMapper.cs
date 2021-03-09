using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Mappers.Abstraction.Base;
using WebApplicationTestTask.Models.Product;

namespace WebApplicationTestTask.Mappers.Implementation
{
    public class ProductMapper : IProductMapper
    {
        public Product MapBack(ProductModel model, Product existing)
        {
            existing.Name = model.Name;
            existing.Price = model.Price;
            existing.AvaliableQuantity = model.AvaliableQuantity;
            existing.ProductCategory = model.ProductCategory;
            existing.Description = model.Description;

            return existing;
        }

        public Product MapBackToEntity(ProductCreationModel model)
        {
            return new Product
            {
                Name = model.Name,
                ProductCategory = model.ProductCategory,
                AvaliableQuantity = model.AvaliableQuantity,
                Price = model.Price,
                Description = model.Description,
            };
        }

        public ProductModel MapToModel(Product entity)
        {
            return new ProductModel
            {
                Id = entity.Id,
                AvaliableQuantity = entity.AvaliableQuantity,
                CreatedDate = entity.CreationDate,
                Name = entity.Name,
                Price = entity.Price,
                ProductCategory = entity.ProductCategory,
                Description = entity.Description
            };
        }
    }
}
