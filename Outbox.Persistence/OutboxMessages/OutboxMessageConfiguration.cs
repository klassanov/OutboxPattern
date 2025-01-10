using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Outbox.Domain.OutboxMessages;

namespace Outbox.Persistence.OutboxMessages
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages");
            builder.HasKey(outbox => outbox.Id);
            builder.Property(outbox => outbox.Content).HasColumnType("JSONB");
        }
    }
}
