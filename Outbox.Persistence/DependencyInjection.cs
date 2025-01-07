using Microsoft.Extensions.DependencyInjection;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Persistence.Repositories;

namespace Outbox.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IOrdersRepository, OrdersRepository>();

            return services;
        }
    }
}
