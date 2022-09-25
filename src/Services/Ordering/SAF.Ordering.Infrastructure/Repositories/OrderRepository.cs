using Microsoft.EntityFrameworkCore;
using SAF.Ordering.Application.Contracts.Persistance;
using SAF.Ordering.Domain.Entities;
using SAF.Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAF.Ordering.Infrastructure.Repositories;

internal class OrderRepository : AsyncRepository<Order>, IOrderRepository
{
	public OrderRepository(DatabaseContext databaseContext): base(databaseContext)
	{
	}

	// TODO: Add cancellation mechanism
	public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName) =>
		await databaseContext.Orders
			.Where(current => current.UserName == userName)
			.ToListAsync();
}
