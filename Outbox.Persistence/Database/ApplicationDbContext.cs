using Microsoft.EntityFrameworkCore;
using Outbox.Application.Abstractions.Database;
using Outbox.Domain.Orders;

namespace Outbox.Persistence.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
    }
}
