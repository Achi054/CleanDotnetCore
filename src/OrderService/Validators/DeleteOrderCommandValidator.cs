using FluentValidation;
using OrderService.Commands;

namespace OrderService.Validators
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator() => RuleFor(x => x.OrderId).NotEmpty();
    }
}
