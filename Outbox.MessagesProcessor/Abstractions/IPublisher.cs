using Outbox.MessagesProcessor.Models;

namespace Outbox.MessagesProcessor.Abstractions
{
    public interface IPublisher
    {
        void PublishMessage(OutboxMessage message);
    }
}
