namespace SAF.Ordering.Application.Exceptions;

public class XNotFoundException : ApplicationException
{
	public XNotFoundException(string name, object key)
		: base($"Entity \"{name}\" (${key}) was not found")
	{

	}
}
