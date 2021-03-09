using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApplicationTestTask.Entities.Enums;

namespace WebApplicationTestTask.Entities
{
    public class Order
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalCost { get; set; }
        public string Comment { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
