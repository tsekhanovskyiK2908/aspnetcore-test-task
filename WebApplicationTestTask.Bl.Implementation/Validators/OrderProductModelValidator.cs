using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Models.OrderProduct;

namespace WebApplicationTestTask.Bl.Implementation.Validators
{
    public class OrderProductModelValidator : AbstractValidator<OrderProductModel>
    {
        public OrderProductModelValidator()
        {
            RuleFor(op => op.ProductId).GreaterThan(0);
            RuleFor(op => op.Quantity).GreaterThan(0);
        }
    }
}
