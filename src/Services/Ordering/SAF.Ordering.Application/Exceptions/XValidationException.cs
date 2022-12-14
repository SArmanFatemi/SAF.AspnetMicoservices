using FluentValidation.Results;

namespace SAF.Ordering.Application.Exceptions;

internal class XValidationException : ApplicationException
{
	public XValidationException()
		:base("One or more validation failures have accured")
	{
		Errors = new Dictionary<string, string[]>();
	}

	public XValidationException(IEnumerable<ValidationFailure> failures)
		: this()
	{
		Errors = failures
			.GroupBy(current => current.PropertyName, current => current.ErrorMessage)
			.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
	}

	public IDictionary<string, string[]> Errors { get; }
}
