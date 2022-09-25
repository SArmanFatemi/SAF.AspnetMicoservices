using FluentValidation;
using MediatR;
using SAF.Ordering.Application.Exceptions;

namespace SAF.Ordering.Application.Behaviors;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> validators;

	public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
	{
		this.validators = validators;
	}

	public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
	{
		if (validators.Any())
		{
			var validationContext = new ValidationContext<TRequest>(request);
			var validationResult = await Task.WhenAll(validators.Select(current => current.ValidateAsync(validationContext, cancellationToken)));
			var failures = validationResult.SelectMany(current => current.Errors).Where(current => current is not null).ToList();

			if (failures.Any()) throw new XValidationException(failures);
		}

		return await next();
	}
}
