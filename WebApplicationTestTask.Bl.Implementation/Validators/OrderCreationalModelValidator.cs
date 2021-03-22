using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Models.Order;
using WebApplicationTestTask.Models.OrderProduct;

namespace WebApplicationTestTask.Bl.Implementation.Validators
{
    public class OrderCreationalModelValidator : AbstractValidator<OrderCreationalModel>
    {
        private IValidator<OrderProductModel> _orderProductModelValidator;
        public OrderCreationalModelValidator(IValidator<OrderProductModel> orderProductModelValidator)
        {
            _orderProductModelValidator = orderProductModelValidator;
            RuleFor(o => o.CustomerId).GreaterThan(0);
            RuleFor(o => o.OrderProductModels).NotNull();
            RuleForEach(o => o.OrderProductModels).SetValidator(_orderProductModelValidator);
        }
    }
}
