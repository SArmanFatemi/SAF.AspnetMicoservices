using FluentValidation;

namespace SAF.Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
	public CheckoutOrderCommandValidator()
	{
		RuleFor(current => current.UserName)
			.NotEmpty()
			.NotNull()
			.MaximumLength(50);

		RuleFor(current => current.EmailAddress)
			.NotEmpty()
			.EmailAddress();

		RuleFor(current => current.TotalPrice)
			.GreaterThanOrEqualTo(0);
	}
}
