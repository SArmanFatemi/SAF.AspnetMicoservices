namespace SAF.Ordering.Domain.Common;

public abstract class BaseEntity
{
	// TODO: Resolve null problem
	public int Id { get; protected set; }

	public string CreateBy { get; set; }

	public DateTime CreateDateTime { get; set; }

	public string LastModifyBy { get; set; }

	public DateTime LastModificationDateTime { get; set; }
}
