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
           return createdOrder.Entity.Id;
        }

        public async Task<Order?> GetOrderById(Guid id)
        {
            return await dbContext.Orders.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
           return await dbContext.SaveChangesAsync();
        }
    }
}
