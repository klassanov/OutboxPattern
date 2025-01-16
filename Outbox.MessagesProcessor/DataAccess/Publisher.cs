using Outbox.Domain.OutboxMessages;
using Outbox.MessagesProcessor.Abstractions;

namespace Outbox.MessagesProcessor.DataAccess
{
    public class Publisher(ILogger<Publisher> logger) : IPublisher
    {
        public void PublishMessage(object message)
        {
            logger.LogInformation("Publishing {messageType}, {payload}", message.GetType().Name, message.ToString());
        }
    }
}
