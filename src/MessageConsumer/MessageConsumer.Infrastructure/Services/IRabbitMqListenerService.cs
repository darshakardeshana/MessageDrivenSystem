namespace MessageConsumer.Infrastructure.Services;

public interface IRabbitMqListenerService
{
    Task StartListeningAsync(CancellationToken cancellationToken);
}