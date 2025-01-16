using Outbox.Domain.OutboxMessages;

namespace Outbox.MessagesProcessor.Abstractions
{
    public interface IOutboxMessagesProcessor
    {
        Task ProcessOutboxMessages();
    }
}
