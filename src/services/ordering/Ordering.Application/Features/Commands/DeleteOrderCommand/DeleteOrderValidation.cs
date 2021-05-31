using FluentValidation;
using Ordering.Application.Features.Commands.CheckoutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.DeleteOrder
{
    public class CheckoutOrderValidation : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not be more than 50 charcter");

            RuleFor(x => x.TotalPrice)
                .NotEmpty().WithMessage("Total price is required");
        }
    }
}
