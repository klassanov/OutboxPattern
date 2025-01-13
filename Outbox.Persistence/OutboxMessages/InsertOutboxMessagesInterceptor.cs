using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Outbox.Application.Extensions;
using Outbox.Domain.Common;
using Outbox.Domain.OutboxMessages;

namespace Outbox.Persistence.OutboxMessages
{
    public class InsertOutboxMessagesInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                await InsertOutboxMessages(eventData.Context);
            }


            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task InsertOutboxMessages(DbContext context)
        {
            var outboxMessages =
            context.ChangeTracker
                   .Entries<DomainEntity>()
                   .Select(entry => entry.Entity)
                   .SelectMany(domainEntity =>
                   {
                       //Internally creates a new List and returns it, so it does not point to the same reference
                       var domainEvents = domainEntity.DomainEvents;
                       domainEntity.ClearDomainEvents();
                       return domainEvents;
                   })
                   .Select(domainEvent => {
                       return domainEvent.ToOutboxMessage();
                   })
                   .ToList();


           await context.Set<OutboxMessage>().AddRangeAsync(outboxMessages);
        }
    }
}
