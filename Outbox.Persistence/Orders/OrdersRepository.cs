using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outbox.Application.Abstractions.Database;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Domain.Orders;

namespace Outbox.Persistence.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IApplicationDbContext dbContext;

        public OrdersRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateOrder(Order order)
        {
           var createdOrder =  await dbContext.Orders.AddAsync(order);
           await dbContext.SaveChangesAsync();
           return createdOrder.Entity.Id;
        }
    }
}
