using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationTestTask.Models.Customer
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal TotalOrderedCost { get; set; }
        public int OrdersCount { get; set; }
    }
}
