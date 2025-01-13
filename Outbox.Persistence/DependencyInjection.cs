using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Application.Abstractions.Database;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Persistence.Database;
using Outbox.Persistence.Orders;
using Outbox.Persistence.OutboxMessages;

namespace Outbox.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("PostgresDocker");

            services.AddSingleton<InsertOutboxMessagesInterceptor>();

            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((serviceProvider, options) =>
            {
                options.UseNpgsql(connectionString)
                       .AddInterceptors(
                            serviceProvider.GetRequiredService<InsertOutboxMessagesInterceptor>());

            });

            services.AddDatabaseDeveloperPageExceptionFilter();


            return services;
        }
    }
}
