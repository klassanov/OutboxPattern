using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Domain.Orders;

namespace Outbox.Persistence.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public Task<Guid> CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
