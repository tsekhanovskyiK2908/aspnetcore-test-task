﻿using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Entities.Enums;

namespace WebApplicationTestTask.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int AvaliableQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
    }
}
