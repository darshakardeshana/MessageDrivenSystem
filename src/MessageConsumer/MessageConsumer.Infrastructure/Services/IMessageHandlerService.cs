using MessageConsumer.Domain.Entities;

namespace MessageConsumer.Infrastructure.Services
{
    public interface IMessageHandlerService
    {
        void ProcessMessage(Message message);
    }
}
