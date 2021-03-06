﻿using FluentValidation;
using OrderService.Commands;

namespace OrderService.Validators
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order).NotNull();

            RuleFor(x => x.Order.Id).NotEmpty();
            RuleFor(x => x.Order.Name).NotEmpty();
            RuleFor(x => x.Order.Quantity).GreaterThan(0);
            RuleFor(x => x.Order.TimeStamp).NotNull();
        }
    }
}
