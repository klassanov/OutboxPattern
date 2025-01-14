using Dapper;
using Npgsql;
using Outbox.MessagesProcessor.Abstractions;
using Outbox.MessagesProcessor.Models;

namespace Outbox.MessagesProcessor.DataAccess
{
    public class OutboxRepository(NpgsqlDataSource dataSource) : IOutboxRepository
    {
        private static string sqlQuery = @"
                SELECT ""Id"", ""Type"", ""Content"", ""OccurredOnUtc"", ""ProcessedOnUtc"", ""Error""
                FROM public.""OutboxMessages""
                WHERE ""ProcessedOnUtc"" IS NULL
                ORDER BY ""OccurredOnUtc"" DESC;";

        public async Task<IEnumerable<OutboxMessage>> GetOutboxMessages()
        {
            using (var connection = await dataSource.OpenConnectionAsync())
            {
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    return await connection.QueryAsync<OutboxMessage>(sqlQuery);
                }
            }
        }
    }
}
