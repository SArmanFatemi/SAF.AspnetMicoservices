using MediatR;
using Microsoft.Extensions.Logging;

namespace SAF.Ordering.Application.Behaviors;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly ILogger<TRequest> logger;

	public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
	{
		this.logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
	{
		try
		{
			return await next();
		}
		catch (Exception exception)
		{
			var requestName = typeof(TRequest).Name;
			logger.LogError(exception, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
			throw;
		}
	}
}
