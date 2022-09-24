using SAF.Ordering.Domain.Entities;

namespace SAF.Ordering.Application.Contracts.Persistance;

public interface IOrderRepository : IAsyncRepository<Order>
{
	Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
}
