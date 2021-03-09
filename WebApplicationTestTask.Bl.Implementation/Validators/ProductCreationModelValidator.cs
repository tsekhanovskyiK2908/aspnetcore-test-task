using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Models.Product;

namespace WebApplicationTestTask.Bl.Implementation.Validators
{
    public class ProductCreationModelValidator : AbstractValidator<ProductCreationModel>
    {
        public ProductCreationModelValidator()
        {
            RuleFor(pcm => pcm.Name).NotNull()
                                    .NotEmpty();

            RuleFor(pcm => pcm.Price).NotEqual(0m);
                                    
        }
    }
}
