using Dapper;
using Npgsql;
using Outbox.MessagesProcessor.Abstractions;
using Outbox.MessagesProcessor.Models;

namespace Outbox.MessagesProcessor.DataAccess
{
    public class OutboxMessagesProcessor(NpgsqlDataSource dataSource, IPublisher publisher) : IOutboxMessagesProcessor
    {
        private static string sqlQuery = @"
                SELECT ""Id"", ""Type"", ""Content"", ""OccurredOnUtc"", ""ProcessedOnUtc"", ""Error""
                FROM public.""OutboxMessages""
                WHERE ""ProcessedOnUtc"" IS NULL
                ORDER BY ""OccurredOnUtc"" DESC;";

        public async Task<IEnumerable<OutboxMessage>> ProcessOutboxMessages()
        {
            using (var connection = await dataSource.OpenConnectionAsync())
            {
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    var messages = await connection.QueryAsync<OutboxMessage>(sqlQuery);
                    foreach (var message in messages)
                    {
                        try
                        {

                            publisher.PublishMessage(message);
                        }
                        catch (Exception ex)
                        {
                        
                        }


                    }
                }
            }

            return null;
        }
    }
}
