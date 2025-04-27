using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using MessageConsumer.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageConsumer.Infrastructure.Services.Implementations;

public class RabbitMqListenerService : IRabbitMqListenerService
{
    private readonly IMessageHandlerService _messageHandlerService;
    private readonly ConnectionFactory _factory;
    private const string QueueName = "message-queue";

    public RabbitMqListenerService(IMessageHandlerService messageHandlerService)
    {
        _messageHandlerService = messageHandlerService;

        _factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            UserName = "guest",
            Password = "guest"
        };
    }

    public async Task StartListeningAsync(CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = await _factory.CreateConnectionAsync(cancellationToken);
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageToProcess = Encoding.UTF8.GetString(body);

                Message message = JsonSerializer.Deserialize<Message>(messageToProcess);
                _messageHandlerService.ProcessMessage(message);

                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: true,
                consumer: consumer,
                cancellationToken: cancellationToken);

            Console.WriteLine($"[Consumer] Listening for messages on queue '{QueueName}'...");

            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("[Consumer] Listening stopped due to cancellation.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Consumer] An error occurred while starting or listening: {ex.Message}");
        }
    }
}