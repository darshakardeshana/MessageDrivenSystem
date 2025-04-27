using MessageProducer.Domain.Entities;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessageProducer.Infrastructure.Services.Implementations;

public class RabbitMqService : IRabbitMqService
{
    private readonly ConnectionFactory _factory = new ConnectionFactory
    {
        HostName = "rabbitmq",
        UserName = "guest",
        Password = "guest"
    };

    private const string QueueName = "message-queue";

    public async Task PublishAsync(Message message)
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: QueueName,
            mandatory: false,
            body: body
        );
    }
}