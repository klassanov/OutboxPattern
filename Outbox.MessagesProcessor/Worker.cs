using Outbox.MessagesProcessor.Abstractions;

namespace Outbox.MessagesProcessor
{
    public class Worker(ILogger<Worker> logger, IOutboxMessagesProcessor outboxRepository, IPublisher publisher) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await outboxRepository.ProcessOutboxMessages();

                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
