using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Application.Abstractions.Database;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Persistence.Database;
using Outbox.Persistence.Orders;

namespace Outbox.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("PostgresDocker");

            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();


            return services;
        }
    }
}
