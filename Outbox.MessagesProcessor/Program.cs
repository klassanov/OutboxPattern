using Npgsql;
using Outbox.MessagesProcessor;
using Outbox.MessagesProcessor.Abstractions;
using Outbox.MessagesProcessor.DataAccess;

var builder = Host.CreateApplicationBuilder(args);


var connString = builder.Configuration.GetConnectionString("PostgresDocker");
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
var npgsqlDataSource = dataSourceBuilder.Build();

builder.Services.AddSingleton<NpgsqlDataSource>(npgsqlDataSource);
builder.Services.AddSingleton<IOutboxRepository, OutboxRepository>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
