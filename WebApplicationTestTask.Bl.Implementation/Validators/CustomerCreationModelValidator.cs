using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTask.Models.Customer;

namespace WebApplicationTestTask.Bl.Implementation.Validators
{
    public class CustomerCreationModelValidator : AbstractValidator<CustomerCreationModel>
    {
        public CustomerCreationModelValidator()
        {
            RuleFor(ccm => ccm.Name).NotNull().NotEmpty();
            RuleFor(ccm => ccm.Address).NotNull().NotEmpty();
        }
    }
}
