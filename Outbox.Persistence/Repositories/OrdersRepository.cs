using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outbox.Application.Abstractions.Database;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Domain.Orders;

namespace Outbox.Persistence.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IApplicationDbContext dbContext;

        public OrdersRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Guid> CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
