using Outbox.Domain.OutboxMessages;

namespace Outbox.MessagesProcessor.Abstractions
{
    public interface IPublisher
    {
        void PublishMessage(object message);
    }
}
