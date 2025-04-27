using MessageProducer.Domain.Entities;

namespace MessageProducer.Infrastructure.Services;

public interface IRabbitMqService
{
    Task PublishAsync(Message message);
}