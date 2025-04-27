using MessageConsumer.Infrastructure.Services;

namespace MessageConsumer.WebApi
{
    public class MessageBackgroundConsumer : BackgroundService
    {
        private readonly IRabbitMqListenerService _rabbitMqListenerService;

        public MessageBackgroundConsumer(IRabbitMqListenerService rabbitMqListenerService)
        {
            _rabbitMqListenerService = rabbitMqListenerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMqListenerService.StartListeningAsync(stoppingToken);
        }
    }
}
