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

        private static string UpdateProcessedOutboxMessageCommand = @"
                UPDATE public.""OutboxMessages""
                SET ""ProcessedOnUtc"" = @ProcessedOnUtc
                WHERE ""Id"" = @Id";

        private static string UpdateFailedOutboxMessageCommand = @"
                UPDATE public.""OutboxMessages""
                SET ""Error"" = @Error, ""ProcessedOnUtc"" = @ProcessedOnUtc
                WHERE ""Id"" = @Id";


        public async Task ProcessOutboxMessages()
        {
            using (var connection = await dataSource.OpenConnectionAsync())
            {
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    var messages = await connection.QueryAsync<OutboxMessage>(
                        sql: GetOutboxMessagesQuery,
                        transaction: transaction);

                    foreach (var message in messages)
                    {
                        try
                        {
                            var messageType = DomainAssembly.GetType(message.Type);
                            var jsonMessage = JsonSerializer.Deserialize(message.Content, messageType);

                            publisher.PublishMessage(jsonMessage);

                            await connection.ExecuteAsync(
                                sql: UpdateProcessedOutboxMessageCommand,
                                param: new { ProcessedOnUtc = DateTime.UtcNow, Id = message.Id },
                                transaction: transaction);
                        }
                        catch (Exception ex)
                        {
                            await connection.ExecuteAsync(
                               sql: UpdateFailedOutboxMessageCommand,
                               param: new { Error = ex.Message, ProcessedOnUtc = DateTime.UtcNow, Id = message.Id },
                               transaction: transaction);
                        }


                    }

                    await transaction.CommitAsync();
                }
            }
        }
    }
}
