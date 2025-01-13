namespace Outbox.Domain.OutboxMessages
{
    public class OutboxMessage
    {
        public OutboxMessage()
        {
            this.Id = Guid.CreateVersion7();
            OccurredOnUtc = DateTime.UtcNow;
        }

        public Guid Id { get; init; }
        public required string Type { get; init; }
        public required string Content { get; init; }
        public DateTime OccurredOnUtc { get; init; }
        public DateTime? ProcessedOnUtc { get; set; }
        public string? Error { get; set; }
    }
}
