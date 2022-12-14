namespace SAF.Ordering.Domain.Common;

internal abstract class ValueObject
{
	protected static bool EqualOperator(ValueObject left, ValueObject right)
	{
		if (left is null ^ right is null)
		{
			return false;
		}

		return left?.Equals(right) is not false;
	}

	protected static bool NotEqualOperator(ValueObject left, ValueObject right)
	{
		return EqualOperator(left, right) is false;
	}

	protected abstract IEnumerable<object> GetEqualityComponents();

	public override bool Equals(object? obj)
	{
		if (obj is null || obj.GetType() != GetType())
		{
			return false;
		}

		var other = (ValueObject)obj;
		return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
	}

	public override int GetHashCode()
	{
		return GetEqualityComponents()
			.Select(x => x != null ? x.GetHashCode() : 0)
			.Aggregate((x, y) => x ^ y);
	}
}
