using FluentValidation;

namespace SAF.Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
	public UpdateOrderCommandValidator()
	{
		RuleFor(current => current.UserName)
			.NotEmpty()
			.NotNull()
			.MaximumLength(50);

		RuleFor(current => current.EmailAddress)
			.NotEmpty()
			.EmailAddress();

		RuleFor(current => current.TotalPrice)
			.NotEmpty()
			.GreaterThan(0);
	}
}
