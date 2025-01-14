using Outbox.MessagesProcessor.Abstractions;

namespace Outbox.MessagesProcessor
{
    public class Worker(ILogger<Worker> logger, IOutboxRepository outboxRepository) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var messages = await outboxRepository.GetOutboxMessages();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
