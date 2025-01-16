using System.Reflection;
using System.Text.Json;
using Dapper;
using Npgsql;
using Outbox.Domain.OutboxMessages;
using Outbox.MessagesProcessor.Abstractions;

namespace Outbox.MessagesProcessor.DataAccess
{
    public class OutboxMessagesProcessor(NpgsqlDataSource dataSource, IPublisher publisher) : IOutboxMessagesProcessor
    {
        private static Assembly DomainAssembly = typeof(OutboxMessage).Assembly;

        private static string GetOutboxMessagesQuery = @"
                SELECT ""Id"", ""Type"", ""Content"", ""OccurredOnUtc"", ""ProcessedOnUtc"", ""Error""
                FROM public.""OutboxMessages""
                WHERE ""ProcessedOnUtc"" IS NULL
                ORDER BY ""OccurredOnUtc"" DESC;";

        private static string UpdateOutboxMessagesQuery = @"";

        public async Task<IEnumerable<OutboxMessage>> ProcessOutboxMessages()
        {
            using (var connection = await dataSource.OpenConnectionAsync())
            {
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    var messages = await connection.QueryAsync<OutboxMessage>(GetOutboxMessagesQuery, transaction: transaction);
                    foreach (var message in messages)
                    {
                        try
                        {
                            var messageType = DomainAssembly.GetType(message.Type);
                            var jsonMessage = JsonSerializer.Deserialize(message.Content, messageType);

                            publisher.PublishMessage(jsonMessage);
                        }
                        catch (Exception ex)
                        {

                        }


                    }

                    await transaction.CommitAsync();
                }
            }

            return null;
        }
    }
}
