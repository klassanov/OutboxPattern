using Microsoft.EntityFrameworkCore;
using Outbox.Domain.Orders;

namespace Outbox.Application.Abstractions.Database
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; set; }
    }
}
