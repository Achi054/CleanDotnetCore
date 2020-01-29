using FluentValidation;
using OrderService.Queries;

namespace OrderService.Validators
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator() => RuleFor(x => x.OrderId).NotEmpty();
    }
}
