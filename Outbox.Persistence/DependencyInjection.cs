using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Application.Abstractions.Database;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Persistence.Database;
using Outbox.Persistence.Repositories;

namespace Outbox.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql("PostgresDocker");
            });


            return services;
        }
    }
}
