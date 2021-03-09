using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Models.Product;

namespace WebApplicationTestTask.Bl.Implementation.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(p => p.Name).NotNull()
                                .NotEmpty();

            RuleFor(p => p.Price).NotEqual(0m);
        }
    }
}
