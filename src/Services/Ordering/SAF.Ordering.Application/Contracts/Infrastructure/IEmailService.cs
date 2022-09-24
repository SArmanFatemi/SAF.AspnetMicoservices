using SAF.Ordering.Application.Models;

namespace SAF.Ordering.Application.Contracts.Infrastructure;

public interface IEmailService
{
	Task<bool> SendEmail(Email email);
}
